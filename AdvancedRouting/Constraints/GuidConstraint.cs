namespace AdvancedRouting.Constraints
{
    public class GuidConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values.TryGetValue(routeKey, out object value))
            {
                if (Guid.TryParse(value.ToString(), out Guid guid))
                {
                    return true;
                }
            }
            return false;
        }
    }
}