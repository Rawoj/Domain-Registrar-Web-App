using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace DomainRegistrarWebApp.Authorization
{
    public class UserManager
    {
        public static class UserOperations
        {
            public static OperationAuthorizationRequirement Create =
                new()
                { Name = Constants.CreateOperationName };

            public static OperationAuthorizationRequirement Read =
                new()
                { Name = Constants.ReadOperationName };

            public static OperationAuthorizationRequirement Update =
                new()
                { Name = Constants.UpdateOperationName };

            public static OperationAuthorizationRequirement Delete =
                new()
                { Name = Constants.DeleteOperationName };

            public static OperationAuthorizationRequirement Approve =
                new()
                { Name = Constants.ApproveOperationName };

            public static OperationAuthorizationRequirement Reject =
                new()
                { Name = Constants.RejectOperationName };
        }

        public class Constants
        {
            public const string CreateOperationName = "Create";
            public const string ReadOperationName = "Read";
            public const string UpdateOperationName = "Update";
            public const string DeleteOperationName = "Delete";
            public const string ApproveOperationName = "Approve";
            public const string RejectOperationName = "Reject";

            public const string UserAdministratorsRole = "UserAdministrators";
        }
    }
}