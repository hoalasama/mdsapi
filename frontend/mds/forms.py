from django import forms

class LoginForm(forms.Form):
    username = forms.CharField(max_length=100)
    password = forms.CharField(widget=forms.PasswordInput)

class RegisterForm(forms.Form):
    username = forms.CharField(max_length=100)
    password = forms.CharField(widget=forms.PasswordInput)
    email = forms.EmailField()

class AddCartDetailForm(forms.Form):
    med_id = forms.IntegerField(label='Medicine ID')
    quantity = forms.IntegerField(label='Quantity')

class EditCartDetailForm(forms.Form):
    quantity = forms.IntegerField(label='Quantity')

