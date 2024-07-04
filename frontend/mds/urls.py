from django.conf import settings
from django.urls import path
from . import views

urlpatterns = [
    path('', views.index_view, name='index'),
    path('login/', views.login_view, name='login'),
    path('register/', views.register_view, name='register'),
    path('logout/', views.logout_view, name='logout'),
    path('manage/pharmacy/', views.manage_pharmacy_view, name='manage_pharmacy'),
    path('manage/medicine/', views.manage_medicine_view, name='manage_medicine'),
    path('cart/', views.get_cart_view, name='cart'),
    path('cart/add/', views.add_to_cart_view, name='add_to_cart'),
    path('cart/edit/<int:cart_detail_id>/', views.edit_cart_item_view, name='edit_cart_item'),
    path('cart/delete/<int:cart_detail_id>/', views.delete_cart_item_view, name='delete_cart_item'),
    path('add_to_cart/', views.add_to_cart_from_medicine_view, name='add_to_cart_from_medicine'),
    path('search/', views.search_medicine_view, name='search_medicine'),
    path('medicine/<int:med_id>/', views.medicine_detail_view, name='medicine_detail'),   
    path('add_review/', views.add_review_view, name='add_review'),
    path('orders/', views.orders_view, name='orders_view'),
    path('place_order/', views.place_order, name='place_order'),
    path('pharmacy_medicines/', views.pharmacy_medicines_view, name='pharmacy_medicines'),
    path('edit_medicine/<int:med_id>/', views.edit_medicine_view, name='edit_medicine'),
    path('update_role/', views.update_role_view, name='update_role'),
    path('users/', views.user_list_view, name='user_list'),
    path('pharmacy/orders/', views.pharmacy_orders_view, name='pharmacy_orders'),
    path('pharmacy/orders/update_status/<int:order_id>/<int:status_id>/', views.update_order_status, name='update_order_status'),
]

