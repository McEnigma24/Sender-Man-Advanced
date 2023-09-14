For the proper functionality of the app, follow the instructions:

In order for the application to be able to send emails, Google Gmail needs a account to authorize the send_mail request.

1. Create a gmail account
2. Set up the 2-Step Verification
3. Go to Manage your Google Account -> Security -> 2-Step Verification
on this page find App Passwords and Generete (copy somethere).

Now we only need to modyfie application file.
1. Go to folder Config and open file Email_Send_Authorization.txt
2. paste your gmail and genereted password (WITHOUT SPACE AFTER ":")
gmail:
app_password:

Example:
gmail:example@gmail.com
app_password:2343jdf

AND THAT'S IT, application is ready to use