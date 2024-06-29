import itertools
import json
import requests
import jwt
import os
from django.shortcuts import render, redirect
from django.core.files.storage import FileSystemStorage
from django.conf import settings
from django.http import JsonResponse
from django.contrib.auth import login as auth_login, logout as auth_logout, authenticate
from django.contrib.auth.models import User
from django.contrib.auth.decorators import login_required
from .forms import AddCartDetailForm, EditCartDetailForm, LoginForm, RegisterForm

API_URL = 'https://localhost:7079/api'
EXPECTED_AUDIENCE = "http://localhost:7079"

def logout_view(request):
    auth_logout(request)
    return redirect('index')

def login_view(request):
    if request.method == 'POST':
        form = LoginForm(request.POST)
        if form.is_valid():
            response = requests.post(f'{API_URL}/account/login', json=form.cleaned_data, verify=False)
            if response.status_code == 200:
                user_data = response.json()
                token = user_data['token']
                try:
                    decoded_token = jwt.decode(token, options={"verify_signature": False}, audience=EXPECTED_AUDIENCE)
                except jwt.InvalidAudienceError:
                    return JsonResponse({'error': 'Invalid audience in token'}, status=400)

                user, created = User.objects.get_or_create(username=decoded_token['given_name'])
                if created:
                    user.email = decoded_token['email']
                    user.set_unusable_password() 
                    user.save()

                request.session['username'] = decoded_token['given_name']
                request.session['token'] = token
                request.session['role'] = decoded_token['role']
                request.session['user_id'] = decoded_token['nameid']
                auth_login(request, user)
                return redirect('index')
            else:
                return JsonResponse({'error': 'Invalid credentials'}, status=400)
    elif request.user.is_authenticated:
        return redirect('index')
    else:
        form = LoginForm()
    return render(request, 'login.html', {'form': form})

def register_view(request):
    if request.method == 'POST':
        form = RegisterForm(request.POST)
        if form.is_valid():
            data = {
                "CustomerName": form.cleaned_data["username"],
                "CustomerEmail": form.cleaned_data["email"],
                "CustomerPassword": form.cleaned_data["password"]
            }
            response = requests.post(f'{API_URL}/account/register', json=data, verify=False)
            if response.status_code == 200:
                # return JsonResponse(response.json())
                return redirect('login')
            else:
                print(response.text)
                return JsonResponse({'error': 'Registration failed', 'details': response.text}, status=response.status_code)
    elif request.user.is_authenticated:
        return redirect('index')
    else:
        form = RegisterForm()
    return render(request, 'register.html', {'form': form})

def index_view(request):
    categories_response = requests.get(f'{API_URL}/category', verify=False)
    categories = categories_response.json() if categories_response.status_code == 200 else []

    medicines_response = requests.get(f'{API_URL}/medicine', verify=False)
    medicines = medicines_response.json() if medicines_response.status_code == 200 else []

    return render(request, 'index.html', {
        'categories': categories,
        'medicines': medicines,
        'username': request.session.get('username', None),
        'role': request.session.get('role', None)
    })

def manage_pharmacy_view(request):
    role = request.session.get('role', None)
    if role != 'Admin':
        return redirect('index')
    
    if request.method == 'POST':
        user_id = request.POST.get('user_id')
        data = {
            "pharName": request.POST.get("pharName"),
            "pharPhone": request.POST.get("pharPhone"),
            "pharEmail": request.POST.get("pharEmail"),
            "pharAddress": request.POST.get("pharAddress")
        }
        headers = {'Authorization': f'Bearer {request.session["token"]}'}
        response = requests.post(f'{API_URL}/pharmacy/{user_id}', json=data, verify=False, headers=headers)
        if response.status_code == 201:
            return JsonResponse(response.json())
        else:
            return JsonResponse({'error': 'Failed to create pharmacy', 'details': response.text}, status=response.status_code)
    else:
        users_response = requests.get(f'{API_URL}/account', verify=False)
        if users_response.status_code == 200:
            users = users_response.json()
            user_role_users = [user for user in users if 'User' in user.get('roles', [])]
        else:
            user_role_users = []
        
        return render(request, 'manage_pharmacy.html', {
            'users': user_role_users
        })

@login_required
def manage_medicine_view(request):
    if request.method == 'POST':
        medName = request.POST.get('medName')
        medDesc = request.POST.get('medDesc')
        medDiscount = request.POST.get('medDiscount')
        medRemain = request.POST.get('medRemain')
        medPrice = request.POST.get('medPrice')
        cateId = request.POST.get('cateId')
        medPicture = request.FILES.get('medPicture')

        if medPicture:
            fs = FileSystemStorage(location=os.path.join(settings.BASE_DIR, 'mds', 'static', 'img'))
            filename = fs.save(medPicture.name, medPicture)
            medPictureUrl = f'/static/img/{filename}'
        else:
            medPictureUrl = ''

        pharmacy_id = request.session.get('user_id')
        if not pharmacy_id:
            return JsonResponse({'error': 'Pharmacy ID not found in session'}, status=400)

        headers = {'Authorization': f'Bearer {request.session["token"]}'}
        response = requests.post(f'{API_URL}/medicine/{pharmacy_id}', json={
            'medName': medName,
            'medDesc': medDesc,
            'medDiscount': medDiscount,
            'medRemain': medRemain,
            'medPrice': medPrice,
            'medPicture': medPictureUrl,
            'cateId': cateId,
        }, verify=False, headers=headers)

        if response.status_code in [200, 201]:
            return redirect('manage_medicine')
        else:
            return JsonResponse({'error': 'Error saving medicine', 'details': response.json()}, status=response.status_code)

    pharmacy_id = request.session.get('user_id')
    response = requests.get(f'{API_URL}/medicine', verify=False)
    medicines = response.json() if response.status_code == 200 else []

    categories_response = requests.get(f'{API_URL}/category', verify=False)
    categories = categories_response.json() if categories_response.status_code == 200 else []

    return render(request, 'manage_medicine.html', {'medicines': medicines, 'categories': categories})

# med_id = request.POST.get('med_id')
        # if med_id:
        #     headers = {'Authorization': f'Bearer {request.session["token"]}'}
        #     response = requests.put(f'{API_URL}/medicine/{med_id}', json={
        #         'medName': medName,
        #         'medDesc': medDesc,
        #         'medDiscount': medDiscount,
        #         'medRemain': medRemain,
        #         'medPrice': medPrice,
        #         'medPicture': medPictureUrl,
        #         'cateId': cateId,
        #     }, verify=False,headers=headers)
        # else:
def add_to_cart_view(request):
    if request.method == 'POST':
        form = AddCartDetailForm(request.POST)
        if form.is_valid():
            token = request.session.get('token')
            headers = {'Authorization': f'Bearer {token}'}
            data = {
                'CustomerId': request.session.get('user_id'),
                'MedId': form.cleaned_data['med_id'],
                'Quantity': form.cleaned_data['quantity']
            }
            response = requests.post(f'{API_URL}/cart/add', json=data,verify=False, headers=headers)
            if response.status_code == 200:
                return redirect('cart')
            else:
                return JsonResponse({'error': 'Failed to add item to cart'}, status=response.status_code)
    else:
        form = AddCartDetailForm()
    return render(request, 'add_to_cart.html', {'form': form})

# views.py
def get_cart_view(request):
    token = request.session.get('token')
    headers = {'Authorization': f'Bearer {token}'}
    customer_id = request.session.get('user_id')
    response = requests.get(f'{API_URL}/cart/{customer_id}', verify=False, headers=headers)
    if response.status_code == 200:
        cart = response.json()
        total = sum(item['med']['medPrice'] * item['quantity'] for item in cart['cartDetails'])
        cart['total'] = total
        return render(request, 'cart.html', {'cart': cart})
    else:
        return JsonResponse({'error': 'Failed to retrieve cart'}, status=response.status_code)


def edit_cart_item_view(request, cart_detail_id):
    if request.method == 'POST':
        quantity = request.POST.get('quantity')
        if quantity is not None:
            try:
                quantity = int(quantity)
                token = request.session.get('token')
                headers = {'Authorization': f'Bearer {token}'}
                data = {
                    'CustomerId': request.session.get('user_id'),
                    'CartDetailId': cart_detail_id,
                    'Quantity': quantity
                }
                response = requests.put(f'{API_URL}/cart/edit', json=data, verify=False, headers=headers)
                if response.status_code == 200:
                    return redirect('cart')
                else:
                    return JsonResponse({'error': 'Failed to edit cart item'}, status=response.status_code)
            except ValueError:
                return JsonResponse({'error': 'Invalid quantity value'}, status=400)
    return JsonResponse({'error': 'Invalid request method'}, status=405)

def delete_cart_item_view(request, cart_detail_id):
    token = request.session.get('token')
    headers = {'Authorization': f'Bearer {token}'}
    data = {
        'CustomerId': request.session.get('user_id'),
        'CartDetailId': cart_detail_id
    }
    response = requests.delete(f'{API_URL}/cart/delete', json=data,verify=False, headers=headers)
    if response.status_code == 204:
        return redirect('cart')
    else:
        return JsonResponse({'error': 'Failed to delete cart item'}, status=response.status_code)
    
def add_to_cart_from_medicine_view(request):
    if request.method == 'POST':
        med_id = request.POST.get('med_id')
        customer_id = request.session.get('user_id')
        if not customer_id:
            return JsonResponse({'error': 'Customer ID not found in session'}, status=400)
        
        headers = {'Authorization': f'Bearer {request.session["token"]}'}
        data = {
            'CustomerId': customer_id,
            'MedId': med_id,
            'Quantity': 1 
        }
        response = requests.post(f'{API_URL}/cart/add', json=data,verify=False, headers=headers)
        
        if response.status_code == 200:
            return redirect('cart')
        else:
            return JsonResponse({'error': 'Failed to add item to cart'}, status=response.status_code)
        
def search_medicine_view(request):
    query = request.GET.get('q', '')
    if query:
        # headers = {'Authorization': f'Bearer {request.session["token"]}'}
        response = requests.get(f'{API_URL}/medicine/search', params={'q': query},  verify=False)
        if response.status_code == 200:
            medicines = response.json()
        else:
            medicines = []
    else:
        medicines = []

    return render(request, 'search_results.html', {'medicines': medicines, 'query': query})

def medicine_detail_view(request, med_id):
    response = requests.get(f'{API_URL}/medicine/{med_id}', verify=False)
    
    if response.status_code == 200:
        medicine = response.json()
        review_response = requests.get(f'{API_URL}/review/{med_id}', verify=False)

        if review_response.status_code == 200:
            reviews = review_response.json()
            customer_data = []
            for review in reviews:
                customer_id = review['customerId']
                customer_response = requests.get(f'{API_URL}/account/{customer_id}', verify=False)
                if customer_response.status_code == 200:
                    customer_data.append(customer_response.json())
                else:
                    customer_data.append({'error': 'Customer data not available'})
            customer_data = sum(customer_data, [])
        else:
            reviews = []
            customer_data = []

        #customer_id= reviews["customerId"]
        
        context = {
            'medicine': medicine,
            'reviews': reviews,
            'customer_data': customer_data
        }
        return render(request, 'medicine_detail.html', context)

def add_review_view(request):
    if request.method == 'POST':
        med_id = request.POST.get('medicine_id')
        review_content = request.POST.get('reviewContent')
        customer_id = request.session.get('user_id')
        api_url = f"{API_URL}/review"
        payload = {
            'customerId': customer_id,
            'medId': med_id,
            'reviewContent': review_content,
        }
        headers = {
            'Authorization': f'Bearer {request.session["token"]}',
            'Content-Type': 'application/json',
        }
        response = requests.post(api_url, json=payload,verify=False, headers=headers)

        if response.status_code == 200:
            return redirect('medicine_detail', med_id=med_id)

    return redirect('index')

# def orders_view(request):
#     token = request.session.get('token')
#     user_id = request.session.get('user_id')

#     if not token or not user_id:
#         return redirect('login')

#     headers = {'Authorization': f'Bearer {token}'}
#     response = requests.get(f'{API_URL}/Order/vieworders/{user_id}', headers=headers, verify=False)

#     if response.status_code == 200:
#         orders = response.json()
#     else:
#         orders = []

#     return render(request, 'orders.html', {'orders': orders})

def orders_view(request):
    token = request.session.get('token')
    user_id = request.session.get('user_id')

    if not token or not user_id:
        return redirect('login')

    headers = {'Authorization': f'Bearer {token}'}
    response = requests.get(f'{API_URL}/Order/vieworders/{user_id}', headers=headers, verify=False)

    if response.status_code == 200:
        orders = response.json()

        for order in orders:
            total_price = 0
            for item in order['orderItems']:
                item_price = item['med']['medPrice'] * item['itemQuantity']
                total_price += item_price
            order['totalPrice'] = total_price

        return render(request, 'orders.html', {'orders': orders})
    else:
        return JsonResponse({'error': 'Failed to retrieve orders'}, status=400)

def place_order(request):
    token = request.session.get('token')
    user_id = request.session.get('user_id')

    if not token or not user_id:
        return redirect('login')

    headers = {'Authorization': f'Bearer {token}'}
    response = requests.post(f'{API_URL}/Order/placeorder/{user_id}', headers=headers, verify=False)

    if response.status_code == 200:
        return redirect('orders_view')
    else:
        return JsonResponse({'error': 'Order placement failed'}, status=400)
    
def pharmacy_medicines_view(request):
    pharmacy_id = request.session.get('user_id')
    if not pharmacy_id:
        return redirect('login')

    headers = {'Authorization': f'Bearer {request.session["token"]}'}
    response = requests.get(f'{API_URL}/pharmacy', headers=headers, verify=False)
    pharmacies = response.json() if response.status_code == 200 else []

    pharmacy = next((p for p in pharmacies if p['customerId'] == pharmacy_id), None)
    medicines = pharmacy['medicines'] if pharmacy else []

    return render(request, 'pharmacy_medicines.html', {'medicines': medicines})

def edit_medicine_view(request, med_id):
    if request.method == 'POST':
        medName = request.POST.get('medName')
        medDesc = request.POST.get('medDesc')
        medDiscount = request.POST.get('medDiscount')
        medRemain = request.POST.get('medRemain')
        medPrice = request.POST.get('medPrice')
        cateId = request.POST.get('cateId')
        medPicture = request.FILES.get('medPicture')

        if medPicture:
            fs = FileSystemStorage(location=os.path.join(settings.BASE_DIR, 'mds', 'static', 'img'))
            filename = fs.save(medPicture.name, medPicture)
            medPictureUrl = f'/static/img/{filename}'
        else:
            medPictureUrl = request.POST.get('medPic')

        headers = {'Authorization': f'Bearer {request.session["token"]}'}
        response = requests.put(f'{API_URL}/medicine/{med_id}', json={
            'medName': medName,
            'medDesc': medDesc,
            'medDiscount': medDiscount,
            'medRemain': medRemain,
            'medPrice': medPrice,
            'medPicture': medPictureUrl,
            'cateId': cateId,
        }, verify=False, headers=headers)

        if response.status_code in [200, 204]:
            return redirect('pharmacy_medicines')
        else:
            return JsonResponse({'error': 'Error updating medicine', 'details': response.json()}, status=response.status_code)

    headers = {'Authorization': f'Bearer {request.session["token"]}'}
    response = requests.get(f'{API_URL}/medicine/{med_id}', headers=headers, verify=False)
    medicine = response.json() if response.status_code == 200 else None

    categories_response = requests.get(f'{API_URL}/category', verify=False)
    categories = categories_response.json() if categories_response.status_code == 200 else []

    return render(request, 'edit_medicine.html', {'medicine': medicine, 'categories': categories})