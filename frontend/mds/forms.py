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

class RoleUpdateForm(forms.Form):
    ROLE_CHOICES = [
        ('Phar', 'Phar'),
        ('User', 'User'),
    ]

    user_id = forms.ChoiceField(choices=[], label='User ID')
    new_role = forms.ChoiceField(choices=ROLE_CHOICES, label='New Role')

    def __init__(self, *args, **kwargs):
        user_choices = kwargs.pop('user_choices', [])
        super().__init__(*args, **kwargs)
        self.fields['user_id'].choices = user_choices