# Language_Specific_Project

Login Info:

UserLogin:
Username- ishan@gmail.com, Password-Ishan123@#

AdminLogin:
Username- admin@gmail.com, Password-Admin123@#

Info About APIs: 
Following are the example endpoints of my APIs:
1. (POST)Login :  https://localhost:7222/api/User/Login or https://localhost:7222/api/User/v1/Login
After the login the access token is received as response. For access following API s the access token should be send as Authorization header with Bearer
ex: Authorization : Bearer <AcessToken>

    Following APIs from 2 to 5 are accessible for both admin and normal user

2. (PUT)Edit user : https://localhost:7222/api/User/EditUser/5aa252be01865d3202ddcbac or https://localhost:7222/api/User/v1/EditUser/5aa252be01865d3202ddcbac
	UserID should be send along with the URL

3. (GET)Get Distance : https://localhost:7222/api/User/GetDistance/5aa252be5d1e07697b16d463?latitude=39.783865&longitude=-32.813122 or
   https://localhost:7222/api/User/v1/GetDistance/5aa252be5d1e07697b16d463?latitude=39.783865&longitude=-32.813122
	UserID,longitude,latitude should be send along with the URL

4. (GET)Search by text : https://localhost:7222/api/User/SearchUser?searchText=kasun or  https://localhost:7222/api/User/v1/SearchUser?searchText=kasun
	search text should be send along with the URL

5. (GET)Get customer list order by zip code : https://localhost:7222/api/User/GetCustomerListByZipCode or https://localhost:7222/api/User/v1/GetCustomerListByZipCode

     For access following api, the user should be logged as an admin. For the view of the data and for login, a seperate dashboard application was developed using react.

6. (GET)View all customer list : https://localhost:7222/api/User/GetAllCustomerList or https://localhost:7222/api/User/v1/GetAllCustomerList
