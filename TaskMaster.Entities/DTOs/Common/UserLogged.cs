namespace TaskMaster.Entities.DTOs.Common
{
    public class UserLogged
    {
        public string UserId
        {
            get; set;
        }

        public bool IsManager
        {
            get; set;
        } = false;
    }
}