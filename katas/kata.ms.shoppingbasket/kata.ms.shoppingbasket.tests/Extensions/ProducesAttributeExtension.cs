using Microsoft.AspNetCore.Mvc;

namespace kata.ms.shoppingbasket.tests.Extensions
{
    // helper for tests. helps validate filter declaration in controllers
    public static class ProducesAttributeExtension
    {
        public static string Value(this ProducesAttribute self)
        {
            return (self.ContentTypes)[0];
        }
    }
}
