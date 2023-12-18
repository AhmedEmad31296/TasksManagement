using TasksManagement.Debugging;

namespace TasksManagement
{
    public class TasksManagementConsts
    {
        public const string LocalizationSourceName = "TasksManagement";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = false;

        public struct DailyTaskAttachmentPath
        {
            public const string UploadPath = "/wwwroot/uploads/DailyTasks/";
            public const string FolderPath = "//uploads//DailyTasks//";
        }

        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "b4c2e8ce6cc24dfcb3e781af06d5011f";
    }
}
