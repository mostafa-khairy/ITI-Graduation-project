# ITI-Graduation-project

**- A desktop application used by students and Instructors that allows students to know the Courses in which they are enrolled in and their grades and allows them to take exams and their results instantly based on a model answer stored in the database.
Instructors are allowed to add students and different questions for each Course. in addition to viewing different reports of the system**

# in this project 

## 1 - Database :
* contain Stored procedures that insert, update, and delete in any table in the database.

* contain a Stored procedure that Generate an exam for any Course by choosing a number of MCQ questions and a number of T/F Questions and choosing this number randomly from the question table.

* contain a Stored procedure that insert the student answers for each question in the database.

* contain a stored procedure that correct each exam based on the model answer.

## 2- Reports :
* Report that returns the students information according to Department No parameter.
*  Report that takes the student ID and returns the grades of the student in all courses.
* Report that takes the instructor ID and returns the name of the courses that he teaches and the number of student per course.
* Report that takes course ID and returns its topics.  
* Report that takes exam number and returns the Questions in it.
* Report that takes exam number and the student ID then returns the Questions in this exam with the student answers. 

## 3- Desktop application using C# :
The desktop application has two different types of users, each
user can log in by Email and Password saved in a database.
### 1. Student :
- Can show grades and perform exam.
### 2. Instructor :
- can add new student.
- can add new Question.
- show reports.

## 4- Dashboard :
* dashboard to summarize the inter-system and connected to Facebook API to get the Student picture. 


# Programs used in the project:
1. Microsoft SQL Server
2. Microsoft Visual Studio for desktop application.
3. Microsoft Power BI.
4. SQL Server Reporting Services (SSRS).
5. Facebook Graph API.
