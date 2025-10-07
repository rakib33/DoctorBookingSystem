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

#### Clinic :

```
public class Clinic : BaseEntity
{
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(200)]
    public string Address { get; set; }

    [StringLength(15)]
    public string PhoneNumber { get; set; }

    [StringLength(50)]
    public string Email { get; set; }

    [StringLength(200)]
    public string Website { get; set; }
    // Navigation properties
    public virtual ICollection<Doctor> Doctors { get; set; }
      
}
```

#### Doctor :

```
public class Doctor : BaseEntity
{
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(200)]
    public string Specialization { get; set; }

    [StringLength(15)]
    public string PhoneNumber { get; set; }

    [StringLength(50)]
    public string Email { get; set; }
    public int ClinicId { get; set; } // Foreign key to Clinic entity
    public Clinic Clinic { get; set; } // Navigation property to Clinic entity
}
```
 
#### Patient :

```
public class Patient : BaseEntity<Guid>
 {
     [StringLength(200)]
     public string Name { get; set; }
       
     [StringLength(15)]
     public string PhoneNumber { get; set; }

     [StringLength(50)]
     public string Email { get; set; }
     public DateTime DateOfBirth { get; set; }

     [StringLength(500)]
     public string Address { get; set; }
     // Navigation property for appointments
     public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
 }
```

#### Schedule:

```
public class ScheduleSlot : BaseEntity<Guid>
 {
     public DateTime StartTime { get; set; }
     public DateTime EndTime { get; set; }
     public int DoctorId { get; set; } // Foreign key to Doctor entity
     public Doctor Doctor { get; set; } // Navigation property to Doctor entity
     public bool IsBooked { get; set; } // Indicates if the slot is booked
     public Appointment Appointment { get; set; }
 }
```

#### Appointment: 

```
public class Appointment : BaseEntity<Guid>
 {
     public DateTime AppointmentDate { get; set; }
     public int DoctorId { get; set; } // Foreign key to Doctor entity
     public Doctor Doctor { get; set; } // Navigation property to Doctor entity
     public Guid PatientId { get; set; } // Foreign key to Patient entity
     public Patient Patient { get; set; } // Navigation property to Patient entity
     public AppointmentStatus Status { get; set; } // Status of the appointment
     public string Notes { get; set; } // Additional notes for the appointment
     public Guid ScheduleSlotId { get; set; } // Foreign key to ScheduleSlot entity
     // Navigation property for schedule slot
     public ScheduleSlot ScheduleSlot { get; set; }

 }
```

### Relational database schema 

#### Clinics Schema :

```
CREATE TABLE [dbo].[Clinics] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (200) NOT NULL,
    [Address]          NVARCHAR (200) NOT NULL,
    [PhoneNumber]      NVARCHAR (15)  NOT NULL,
    [Email]            NVARCHAR (50)  NOT NULL,
    [Website]          NVARCHAR (200) NOT NULL,
    [CreatedDate]      DATETIME2 (7)  NOT NULL,
    [CreatedBy]        NVARCHAR (MAX) NOT NULL,
    [LastModifiedDate] DATETIME2 (7)  NULL,
    [UpdatedBy]        NVARCHAR (MAX) NOT NULL,
    [IsActive]         BIT            NOT NULL,
    CONSTRAINT [PK_Clinics] PRIMARY KEY CLUSTERED ([Id] ASC)
);
```

#### Doctors Schema :

```
CREATE TABLE [dbo].[Doctors] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (200) NOT NULL,
    [Specialization]   NVARCHAR (100) NOT NULL,
    [PhoneNumber]      NVARCHAR (15)  NOT NULL,
    [Email]            NVARCHAR (50)  NOT NULL,
    [ClinicId]         INT            NOT NULL,
    [CreatedDate]      DATETIME2 (7)  NOT NULL,
    [CreatedBy]        NVARCHAR (MAX) NOT NULL,
    [LastModifiedDate] DATETIME2 (7)  NULL,
    [UpdatedBy]        NVARCHAR (MAX) NOT NULL,
    [IsActive]         BIT            NOT NULL,
    CONSTRAINT [PK_Doctors] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Doctors_Clinics_ClinicId] FOREIGN KEY ([ClinicId]) REFERENCES [dbo].[Clinics] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Doctors_ClinicId_Name]
    ON [dbo].[Doctors]([ClinicId] ASC, [Name] ASC);

```

#### Patients Schema :

```
CREATE TABLE [dbo].[Patients] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [Name]             NVARCHAR (150)   NOT NULL,
    [PhoneNumber]      NVARCHAR (15)    NOT NULL,
    [Email]            NVARCHAR (200)   NOT NULL,
    [DateOfBirth]      DATETIME2 (7)    NOT NULL,
    [Address]          NVARCHAR (500)   NOT NULL,
    [CreatedDate]      DATETIME2 (7)    NOT NULL,
    [CreatedBy]        NVARCHAR (MAX)   NOT NULL,
    [LastModifiedDate] DATETIME2 (7)    NULL,
    [UpdatedBy]        NVARCHAR (MAX)   NOT NULL,
    [IsActive]         BIT              NOT NULL,
    CONSTRAINT [PK_Patients] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Patients_Email]
    ON [dbo].[Patients]([Email] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Patients_PhoneNumber]
    ON [dbo].[Patients]([PhoneNumber] ASC);
```

#### ScheduleSlots Schema :

```
CREATE TABLE [dbo].[ScheduleSlots] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [StartTime]        DATETIME2 (7)    NOT NULL,
    [EndTime]          DATETIME2 (7)    NOT NULL,
    [DoctorId]         INT              NOT NULL,
    [IsBooked]         BIT              NOT NULL,
    [CreatedDate]      DATETIME2 (7)    NOT NULL,
    [CreatedBy]        NVARCHAR (MAX)   NOT NULL,
    [LastModifiedDate] DATETIME2 (7)    NULL,
    [UpdatedBy]        NVARCHAR (MAX)   NOT NULL,
    [IsActive]         BIT              NOT NULL,
    CONSTRAINT [PK_ScheduleSlots] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ScheduleSlots_Doctors_DoctorId] FOREIGN KEY ([DoctorId]) REFERENCES [dbo].[Doctors] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ScheduleSlots_DoctorId_IsBooked]
    ON [dbo].[ScheduleSlots]([DoctorId] ASC, [IsBooked] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ScheduleSlots_DoctorId_StartTime]
    ON [dbo].[ScheduleSlots]([DoctorId] ASC, [StartTime] ASC);
```

#### Appointments Schema :

```
CREATE TABLE [dbo].[Appointments] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [AppointmentDate]  DATETIME2 (7)    NOT NULL,
    [DoctorId]         INT              NOT NULL,
    [PatientId]        UNIQUEIDENTIFIER NOT NULL,
    [Status]           INT              NOT NULL,
    [Notes]            NVARCHAR (MAX)   NOT NULL,
    [ScheduleSlotId]   UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]      DATETIME2 (7)    NOT NULL,
    [CreatedBy]        NVARCHAR (MAX)   NOT NULL,
    [LastModifiedDate] DATETIME2 (7)    NULL,
    [UpdatedBy]        NVARCHAR (MAX)   NOT NULL,
    [IsActive]         BIT              NOT NULL,
    CONSTRAINT [PK_Appointments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Appointments_Doctors_DoctorId] FOREIGN KEY ([DoctorId]) REFERENCES [dbo].[Doctors] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Appointments_Patients_PatientId] FOREIGN KEY ([PatientId]) REFERENCES [dbo].[Patients] ([Id]),
    CONSTRAINT [FK_Appointments_ScheduleSlots_ScheduleSlotId] FOREIGN KEY ([ScheduleSlotId]) REFERENCES [dbo].[ScheduleSlots] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Appointments_DoctorId]
    ON [dbo].[Appointments]([DoctorId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Appointments_PatientId]
    ON [dbo].[Appointments]([PatientId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Appointments_ScheduleSlotId]
    ON [dbo].[Appointments]([ScheduleSlotId] ASC);

```

#### Code First Database Migration command to generate Database
-	 Add-Migration <migration_name>
-	Update-Database

### System Architecture

Here we use clean architecture. The project structure looks like:
 
-	API Layer  [ DoctorBooking.API ]
-	Service Layer [DoctorBooking.Application]
-	Data Layer [DoctorBooking.Infrastructure]
-	Domain Layer [DoctorBooing.Domain]
-	Background workers [DoctorBooking.BackgroundServices]

#### Diagram or flow showing the booking operation.

<img width="471" height="485" alt="image" src="https://github.com/user-attachments/assets/0fc11787-d07b-4802-aee7-4c0e3d805f64" />

#### Describe how you prevent double bookings and ensure data consistency.

- Each booking we maintain a Schedule Slot Id in database. Before booking a slot we must check this Slot Id and prevent double booking.
-	First create database transaction to start .this ensure database consistency.
-	Check the schedule is booked by checking IsBooked property.
-	If not booked update the slot IsBooked = true;
-	Booking new appointment of this slot.
-	Save all Changes Slot and Appointment table.
-	Commit the transaction.
-	If failed it will rollback automatically.
-	Code block is given here. 

```
  public async Task<bool> BookSlotAsync(Guid slotId, Guid patientId,string notes)
  {
      using var tx = await _db.Database.BeginTransactionAsync();

      var slot = await _db.ScheduleSlots
          .Where(s => s.Id == slotId && !s.IsBooked).FirstOrDefaultAsync();


      if (slot == null) return false;

      //Update the slot to mark it as booked
      slot.IsBooked = true;

      //add the appointment record
      _db.Appointments.Add(new Appointment
      {
          ScheduleSlotId = slotId,
          PatientId = patientId,
          CreatedDate = DateTime.UtcNow,
          CreatedBy = patientId.ToString(),
          DoctorId = slot.DoctorId,
          AppointmentDate = slot.StartTime,
          Notes = notes,
          UpdatedBy = patientId.ToString(),
          LastModifiedDate = DateTime.UtcNow,
          Status = AppointmentStatus.Confirmed
      });

      await _db.SaveChangesAsync();
      await tx.CommitAsync();
      return true;
  }
```

#### Explain Project Layer

-	Data Layer [DoctorBooking.Infrastructure]

1.	Create ApplicationDbContext.cs under Data folder to communicate database and Entity using ORM Entity Framework Core.

```
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    // DbSet properties for your entities
    public DbSet<Clinic> Clinics { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<ScheduleSlot> ScheduleSlots { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ClinicConfiguration());
        modelBuilder.ApplyConfiguration(new DoctorConfiguration());
        modelBuilder.ApplyConfiguration(new PatientConfiguration());
        modelBuilder.ApplyConfiguration(new ScheduleSlotConfiguration());
        modelBuilder.ApplyConfiguration(new AppointmentConfiguration());
        // Additional model configurations can go here
    }
}
``` 

2.	Create Configurations folder. All configuration like indexing, entity relationship mapping (one to one, one to many, many to many) are goes there.

- PatientConfiguration

```
public class PatientConfiguration : IEntityTypeConfiguration<Patient>
 {
     public void Configure(EntityTypeBuilder<Patient> builder)
     {
         builder.HasKey(p => p.Id);

         builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);

         builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(200);

         builder.HasIndex(p => p.Email).IsUnique();

         builder.HasIndex(p => p.PhoneNumber);
     }
 }

```

- ScheduleSlotConfiguration 

```
public class ScheduleSlotConfiguration :  EntityTypeConfiguration<ScheduleSlot>
  {
      public void Configure(EntityTypeBuilder<ScheduleSlot> builder)
      {
          builder.HasKey(s => s.Id);

          builder.Property(s => s.StartTime).IsRequired();
          builder.Property(s => s.EndTime).IsRequired();
          // Prevent overlapping slots
          builder.HasIndex(s => new { s.DoctorId, s.StartTime }).IsUnique();
          // Filtered Index: for fast querying of available slots
          builder.HasIndex(s => new { s.DoctorId, s.IsBooked });
                  
      }
 }
```

- DoctorConfiguration

```
public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Name)
               .IsRequired()
               .HasMaxLength(200);
        builder.Property(d => d.Specialization)
               .HasMaxLength(100);
        builder.HasIndex(d => new { d.ClinicId, d.Name });
    }
}
```

- ClinicConfiguration

```
public class ClinicConfiguration : IEntityTypeConfiguration<Clinic>
{
    public void Configure(EntityTypeBuilder<Clinic> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(200);

        builder.HasMany(c => c.Doctors)
               .WithOne(d => d.Clinic)
               .HasForeignKey(d => d.ClinicId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}

```

- AppointmentConfiguration

```
public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
 {
     public void Configure(EntityTypeBuilder<Appointment> builder)
     {
         builder.HasKey(a => a.Id);

         builder.Property(a => a.CreatedDate).IsRequired();

         builder.Property(a => a.Status)
                .HasConversion<int>()
                .IsRequired();

         builder.HasOne(a => a.ScheduleSlot)
                .WithOne(s => s.Appointment)
                .HasForeignKey<Appointment>(a => a.ScheduleSlotId)
                .OnDelete(DeleteBehavior.Restrict);

         builder.HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

         // Enforce one appointment per slot
         builder.HasIndex(a => a.ScheduleSlotId).IsUnique();

         // Fast retrieval of appointments per patient
         builder.HasIndex(a => a.PatientId);
     }
 }
```

- Each doctor can define available time slots
- Doctor can give available time slot 
 
 <img width="900" height="232" alt="image" src="https://github.com/user-attachments/assets/4da5beb2-a69c-46dc-8c64-c3debd796ed1" />


- Create ISlotRepository interface:

```
public interface ISlotRepository
 {
     Task<bool> AddSlotAsync(int doctorId, DateTime startTime, DateTime endTime);
     Task<IEnumerable<ScheduleSlot>> GetDoctorSlotsAsync(int doctorId);

     Task<IQueryable<ScheduleSlot>> GetAllDoctorSlotsAsync();
 }
```

- Implement interface

``` 
public class SlotRepository : ISlotRepository
{
    private readonly ApplicationDbContext _db;

    public SlotRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<bool> AddSlotAsync(int doctorId, DateTime startTime, DateTime endTime)
    {
        // Define start and end times in UTC
        var startUtc = startTime.ToUniversalTime();
        var endUtc = endTime.ToUniversalTime();

        // Check for overlapping slots
        bool overlaps = await _db.ScheduleSlots.AnyAsync(s =>
                s.DoctorId == doctorId &&
                s.StartTime < endUtc &&
                s.EndTime > startUtc);

        if (overlaps) return false;

        var slot = new ScheduleSlot
        {
            DoctorId = doctorId,
            StartTime = startUtc,
            EndTime = endUtc,
            IsBooked = false,
            CreatedBy = "System", // Assuming system user for slot creation
            CreatedDate = DateTime.UtcNow,
            UpdatedBy = "System", // Assuming system user for slot update
            LastModifiedDate = DateTime.UtcNow
        };

        _db.ScheduleSlots.Add(slot);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<IQueryable<ScheduleSlot>> GetAllDoctorSlotsAsync()
    {
        //eager load doctor details for each slot
        return  _db.ScheduleSlots.AsNoTracking().Include(s => s.Doctor).OrderBy(s => s.StartTime);
    }

    public async Task<IEnumerable<ScheduleSlot>> GetDoctorSlotsAsync(int doctorId)
    {
        //eager load doctor details for each slot
        return await _db.ScheduleSlots.AsNoTracking().Include(s => s.Doctor)
                         .Where(s => s.DoctorId == doctorId)
                         .OrderBy(s => s.StartTime)
                         .ToListAsync();
    }
}
```

- Create controller :

```
    [Route("api/[controller]")]
    [ApiController]
    public class SlotController : ControllerBase
    {

        private readonly ISlotService _slotService;

        public SlotController(ISlotService slotService)
        {
            _slotService = slotService;
        }

        [HttpPost]
        public async Task<IActionResult> AddSlot([FromBody] SlotRequest request)
        {
            var success = await _slotService.AddSlotAsync(request.DoctorId, request.StartTime, request.EndTime);
            if (success)
                return Ok("Slot added successfully");
            else
                return Conflict("Slot overlaps with an existing one");
        }

        [HttpGet("{doctorId}")]
        public async Task<IActionResult> GetDoctorSlots(int doctorId)
        {
            var slots = await _slotService.GetDoctorSlotsAsync(doctorId);
            return Ok(slots);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var slots = await _slotService.GetAllDoctorSlotsAsync();
            return Ok(slots);
        }
    }
```

-	A patient can search and book an available slot.
- Patient can search and booked a slot

- Create interface IBookingRepository

```
public interface IBookingRepository
 {
     /// <summary>
     /// Attempts to book a slot by patient. Returns false if slot is already booked.
     /// </summary>
     Task<bool> BookSlotAsync(Guid slotId, Guid patientId,string notes);

     /// <summary>
     /// Returns all available slots for a doctor on a specific date.
     /// </summary>
     IQueryable<ScheduleSlot> GetAvailableSlots(int doctorId, DateTime date);

     /// <summary>
     /// Gets all upcoming confirmed appointments from a specific point in time.
     /// </summary>
     Task<List<Appointment>> GetUpcomingAppointmentsAsync(DateTime fromUtc);
 }
```

- Implement that interface

```
public class BookingRepository : IBookingRepository
 {
     private readonly ApplicationDbContext _db;
     public BookingRepository(ApplicationDbContext db) => _db = db;

     /// <summary>
     /// performing multiple related operations (e.g., create appointment + update slot status).
     /// </summary>
     /// <param name="slotId"></param>
     /// <param name="patientId"></param>
     /// <param name="notes"></param>
     /// <returns></returns>
     public async Task<bool> BookSlotAsync(Guid slotId, Guid patientId,string notes)
     {
         using var tx = await _db.Database.BeginTransactionAsync();

         var slot = await _db.ScheduleSlots
             .Where(s => s.Id == slotId && !s.IsBooked).FirstOrDefaultAsync();


         if (slot == null) return false;

         //Update the slot to mark it as booked
         slot.IsBooked = true;

         //add the appointment record
         _db.Appointments.Add(new Appointment
         {
             ScheduleSlotId = slotId,
             PatientId = patientId,
             CreatedDate = DateTime.UtcNow,
             CreatedBy = patientId.ToString(),
             DoctorId = slot.DoctorId,
             AppointmentDate = slot.StartTime,
             Notes = notes,
             UpdatedBy = patientId.ToString(),
             LastModifiedDate = DateTime.UtcNow,
             Status = AppointmentStatus.Confirmed
         });

         await _db.SaveChangesAsync();
         await tx.CommitAsync();
         return true;
     }

     public IQueryable<ScheduleSlot> GetAvailableSlots(int doctorId, DateTime date) =>
         _db.ScheduleSlots.AsNoTracking().Include(s=>s.Doctor)
            .Where(s => s.DoctorId == doctorId && !s.IsBooked
                    && s.StartTime.Date == date.Date);

     public Task<List<Appointment>> GetUpcomingAppointmentsAsync(DateTime from) =>
         _db.Appointments.AsNoTracking().Include(a=>a.Doctor)
            .Include(a => a.Patient)
            .Include(a => a.ScheduleSlot)
            .Where(a => a.ScheduleSlot.StartTime >= from && a.Status == AppointmentStatus.Confirmed)
            .ToListAsync();
 }
```

- Create Controller BookingController :

```
[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly IBookingService _svc;

    public BookingController(IBookingService svc) => _svc = svc;

    [HttpGet("slots")]
    public async Task<IActionResult> GetSlots(int doctorId, DateTime date)
    {
        var slots = await _svc.SearchAvailableSlots(doctorId, date);
        return Ok(slots);
    }

    [HttpPost]
    public async Task<IActionResult> Book([FromBody] BookRequest req)
    {
        bool success = await _svc.BookSlotAsync(req.SlotId, req.PatientId,req.Notes);
        return success ? Ok("Booked successful") : Conflict("Slot already booked");
    }
}
```

- Check Time Slot of given Doctor of a particular Date
 <img width="900" height="594" alt="image" src="https://github.com/user-attachments/assets/675dc822-5b3e-412f-a5a8-bcd296e9dc0a" />

 <img width="813" height="734" alt="image" src="https://github.com/user-attachments/assets/1e62aec6-e368-41f7-88b7-61057e8a2fd1" />

- If Slot already booked it show message Slot already booked

  <img width="831" height="630" alt="image" src="https://github.com/user-attachments/assets/2da2ed6e-0ace-4f33-8beb-f1894907722f" />

 
- Create an Appointment by giving Slot Id and Patient Id also additional notes if any
 
<img width="866" height="531" alt="image" src="https://github.com/user-attachments/assets/b615721a-7f38-4505-9a7b-19c6faab2796" />


#### Scalability & Extensibility 

Describe how the system could later support features like:

-	SMS/Email reminders

We can use background scheduler like BackgroundService for email or SMS send for reminder.
Create a project DoctorBooking.BackgroundServices and create Class

```
public class AppointmentReminderWorker : BackgroundService
 {
     private readonly IServiceProvider _serviceProvider;
     private readonly ILogger<AppointmentReminderWorker> _log;

     public AppointmentReminderWorker(IServiceProvider serviceProvider, ILogger<AppointmentReminderWorker> log)
     {
         _serviceProvider = serviceProvider;
         _log = log;
     }

     protected override async Task ExecuteAsync(CancellationToken token)
     {
         while (!token.IsCancellationRequested)
         {
             using var scope = _serviceProvider.CreateScope();

             // it's safe to resolve scoped services like DbContext or BookingService.because
             //scopede service IBookingService can't directly access singleton services.
             var bookingService = scope.ServiceProvider.GetRequiredService<IBookingService>();

             var upcoming = await bookingService.GetUpcomingAppointments(DateTime.UtcNow.AddHours(24));
             foreach (var appt in upcoming)
             {
                 _log.LogInformation($"Reminder: appt {appt.Id} for patient {appt.Patient.Email} at {appt.ScheduleSlot.StartTime}");
                 // Hook for email/SMS calls
             }
             await Task.Delay(TimeSpan.FromMinutes(5), token);
         }
     }
 }

```

-	Online consultations

We can use SignalR real time live consultations.

#### Payment integration
- Payment integration we can create separate module for payment gateway and integrate to our current system. 
- payment we can consume this module through gRPC  and messaging we can use RabbitMQ  for message queue

