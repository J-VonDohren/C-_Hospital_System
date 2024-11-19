namespace Assignment2FinalDesign
{
    public interface IUser
    {
        public abstract void DisplayInfo();// the DisplayInfo method is predefined as an abstract method to ensure all child classes have their own implementation
        public abstract void LogOut();// the LogOut method is predefined as an abstract method to ensure all child classes have their own implementation
    }
}
