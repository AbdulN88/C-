//WIth Linq extentsion method
private static bool IsInAnyRole(this IPrincipal user, List<string> roles)
{
    var userRoles = Roles.GetRolesForUser(user.Identity.Name);

    return userRoles.Any(u => roles.Contains(u));
}

var roles = new List<string> { "Admin", "Author", "Super" };

if (user.IsInAnyRole(roles))
{
    //do something
}

//Without link extentsion method
var roles = new List<string> { "Admin", "Author", "Super" };
var userRoles = Roles.GetRolesForUser(User.Identity.Name);

if (userRoles.Any(u => roles.Contains(u))
{
    //do something
}

///Source:http://stackoverflow.com/questions/14477757/how-can-i-check-if-a-user-is-in-any-one-of-a-few-different-roles-with-mvc4-simpl