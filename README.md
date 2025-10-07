# Clean Architecture .Net 8
This is doctor appointment book system using clean architecture in .Net 8

## Backend System Design Assignment – Mini Appointment Booking System

### 🎯 Objective
- Design a scalable backend system for booking doctor appointments at multiple clinics. 
- Focus on backend architecture, data modeling, and scalability.
  
### 💼 Business Scenario

building the backend of a platform where:
-	Each clinic has multiple doctors.
-	Each doctor can define available time slots.
-	A patient can search and book an available slot.
-	Each slot must be booked by only one patient.
-	New clinics and doctors can be added dynamically.

### 🔍 Tasks Explanation

#### 1. Entity Design & Data Modeling  

-	Identify and define key entities: Create a Blank Solution Project in .Net Core named DoctorBookingSystem 
-	Identify and define key entities under Entities folder.

<img width="455" height="491" alt="image" src="https://github.com/user-attachments/assets/245c3b1e-703b-4708-8fc3-51f6c5ec462c" />

- Creating a BaseEntity Class
```
  //add common properties to this class that you want to be inherited by all entities in the domain layer.
 public class BaseEntity<T>
 {
     public T Id { get; set; }
     public DateTime CreatedDate { get; set; }
     public String CreatedBy { get; set; } 
     public DateTime? LastModifiedDate { get; set; }
     public String UpdatedBy { get; set; }

     [DefaultValue(true)]
     public bool IsActive { get; set; }
 }

 //donot add properties/fields/methods to this class. Do that in the above class.
 public class BaseEntity : BaseEntity<int> { }
```
