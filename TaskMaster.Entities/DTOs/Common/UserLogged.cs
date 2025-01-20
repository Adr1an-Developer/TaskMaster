namespace TaskMaster.Entities.DTOs.Common
{
    public class UserLogged
    {
        public string userId
        {
            get; set;
        }

        public bool IsManager
        {
            get; set;
        } = false;
    }
}