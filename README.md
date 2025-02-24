# C# Hospital System
This repository contains a Hospital System with Two Implementations:
1. **Initial Design** - a design that provides pure functionality
2. **Final Design** - a design that optimises the initial design enhancing performance and readability of code

## Features
- patient/staff Log in and sign up
- Patient Check in
- Surgery scheduling
- floor management

---
## Installation

1. Clone the repository:
   ```sh
   git clone https://github.com/J-VonDohren/C-_Hospital_System
   cd C-_Hospital_System
   ```
   
---

## Initial System Startup
The System utilises RAM which means the log in data will be lost for users once the program is shut down, this will be combatted in the future with the implementation of a database to store all staff and patient data
### User Sign up CLI
### Patient
1. Enter your name
2. Enter your age
3. Enter your Mobile Number
4. Enter your Email
5. Enter your Password
### Floor Manager
1. Enter your name
2. Enter your age
3. Enter your Mobile Number
4. Enter your Email
5. Enter your Password
6. Enter your Staff ID
7. Enter your Floor Number

### Surgeon
1. Enter your name
2. Enter your age
3. Enter your Mobile Number
4. Enter your Email
5. Enter your Password
6. Enter your Staff ID
7. Select a speciality

---
## CLI Usage
### Patient
1. Display my details
2. Change password
3. Check in / check out
4. see room
5. see surgeon
6. see surgery date and time
7. log out
### Floor Manager
1. Display my details
2. Change password
3. assign room to patient
4. assign surgery
5. unassign room
6. log out
### Surgeon
1. Display my details
2. Change password
3. see your list of patients
4. see your schedule
5. perform surgery
6. log out
---
## Contributing
1. Fork the repository
2. Create a feature branch (`git checkout -b feature-name`)
3. Commit your changes (`git commit -m 'Add new feature'`)
4. Push to your branch (`git push origin feature-name`)
5. Open a Pull Request

---
