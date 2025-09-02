namespace ECommerce.Constraints
{
    public class CategoryConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values.TryGetValue(routeKey, out object value))
            {
                string categoryValue = value.ToString();
                if (categoryValue == "Electronics" || categoryValue == "Fashion")
                {
                    return true;
                }
            }
            return false;
        }
    }

}
