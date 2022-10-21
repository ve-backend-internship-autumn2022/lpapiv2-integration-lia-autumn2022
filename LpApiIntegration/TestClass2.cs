using System;

public class Class1
{
    public Class1()
    {


public class Rootobject
    {
        public string ApiVersion { get; set; }
        public Data Data { get; set; }
    }

    public class Data
    {
        public Group[] Groups { get; set; }
        public Parentgroup3[] ParentGroups { get; set; }
        public Referencedata ReferenceData { get; set; }
    }

    public class Referencedata
    {
        public Staffmember[] StaffMembers { get; set; }
        public Student[] Students { get; set; }
        public Grouprole[] GroupRoles { get; set; }
        public Coursedefinition[] CourseDefinitions { get; set; }
    }

    public class Staffmember
    {
        public int Id { get; set; }
        public string NationalRegistrationNumber { get; set; }
        public string Signature { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public object Email2 { get; set; }
        public string MobilePhone { get; set; }
        public bool MayExposeMobilePhoneToStudents { get; set; }
        public object Phone2 { get; set; }
        public bool MayExposePhone2ToStudents { get; set; }
    }

    public class Student
    {
        public int Id { get; set; }
        public string NationalRegistrationNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string MobilePhone { get; set; }
        public Homeaddress HomeAddress { get; set; }
    }

    public class Homeaddress
    {
        public object CO { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
    }

    public class Grouprole
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Coursedefinition
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsInternship { get; set; }
        public int Points { get; set; }
    }

    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime? LifespanFrom { get; set; }
        public DateTime? LifespanUntil { get; set; }
        public Category Category { get; set; }
        public Staffgroupmember[] StaffGroupMembers { get; set; }
        public Studentgroupmember[] StudentGroupMembers { get; set; }
        public Parentgroup ParentGroup { get; set; }
        public Coursedefinition1 CourseDefinition { get; set; }
    }

    public class Category
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class Parentgroup
    {
        public Group1 Group { get; set; }
        public Parentgroup1 ParentGroup { get; set; }
    }

    public class Group1
    {
        public int Id { get; set; }
    }

    public class Parentgroup1
    {
        public Group2 Group { get; set; }
        public Parentgroup2 ParentGroup { get; set; }
    }

    public class Group2
    {
        public int Id { get; set; }
    }

    public class Parentgroup2
    {
        public Group3 Group { get; set; }
    }

    public class Group3
    {
        public int Id { get; set; }
    }

    public class Coursedefinition1
    {
        public int Id { get; set; }
    }

    public class Staffgroupmember
    {
        public Staffmember1 StaffMember { get; set; }
        public Grouprole1[] GroupRoles { get; set; }
        public bool IsGroupManager { get; set; }
    }

    public class Staffmember1
    {
        public int Id { get; set; }
    }

    public class Grouprole1
    {
        public int Id { get; set; }
    }

    public class Studentgroupmember
    {
        public Student1 Student { get; set; }
        public Grouprole2[] GroupRoles { get; set; }
    }

    public class Student1
    {
        public int Id { get; set; }
    }

    public class Grouprole2
    {
        public int Id { get; set; }
    }

    public class Parentgroup3
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime LifespanFrom { get; set; }
        public DateTime LifespanUntil { get; set; }
        public Category1 Category { get; set; }
        public Parentgroup4 ParentGroup { get; set; }
        public Extendedproperty[] ExtendedProperties { get; set; }
        public Staffgroupmember1[] StaffGroupMembers { get; set; }
        public Studentgroupmember1[] StudentGroupMembers { get; set; }
    }

    public class Category1
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    

  

   

    public class Staffgroupmember1
    {
        public Staffmember2 StaffMember { get; set; }
        public Grouprole3[] GroupRoles { get; set; }
        public bool IsGroupManager { get; set; }
    }

    public class Staffmember2
    {
        public int Id { get; set; }
    }

    

    

   

   

}
}
