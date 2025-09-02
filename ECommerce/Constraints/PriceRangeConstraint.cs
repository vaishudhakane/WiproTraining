namespace ECommerce.Constraints
{
    public class PriceRangeConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values.TryGetValue(routeKey, out object value))
            {
                string priceRangeValue = value.ToString();
                if (priceRangeValue == "low" || priceRangeValue == "high")
                {
                    return true;
                }
            }
            return false;
        }
    }
}


