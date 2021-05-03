using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Kabutto
{
    public sealed class HttpRequest
    {
        public string Method;
        public Dictionary<string, string> POST = new(), GET = new(), Headers = new();
        public string Path;

        public string Request;

        public HttpRequest(string request)
        {
            Request = request;
        }

        public void Parse()
        {
            Console.WriteLine(Request);

			List<string> lines = Request.Split("\r\n").ToList();

			// Данные с post-запроса
			List<string> postVariables = new();
			if (lines[lines.Count - 2] == "")
			{
				postVariables = lines.Last().Split("&").ToList();
				lines.RemoveRange(lines.Count - 2, 2);
			}

			lines.RemoveAll(str => str == "");

			// Получение метода и пути
			List<string> firstLine = lines[0].Split(" ").ToList();
			lines.RemoveAt(0);
			Method = firstLine[0];
			Path = firstLine[1];

			// Все http-данные(например Content-Length)
			List<string> temp = new();
			foreach(var item in lines)
			{
				temp = item.Split(": ").ToList();
				Headers.Add(temp[0], temp[1]);
			}

			// Обработка данных post-запроса
			if (Method == "POST")
			{
				temp.Clear();
				foreach (var item in postVariables)
				{
					temp = item.Split("=").ToList();
					POST.Add(temp[0], HttpUtility.UrlDecode(temp[1]));
				}
			}

			// Если в пути присутствует символ "?" - значит после него идут данные get-запроса
			if (Path.Contains("?"))
			{
				temp = Path.Split("?").ToList();
				Path = temp[0];
				string getData = temp[1];

				temp.Clear();
				temp.Add(getData);

				if (getData.Contains("&"))
				{
					temp = getData.Split("&").ToList();
				}

				List<string> temp2;
				foreach (var item in temp)
				{
					temp2 = item.Split("=").ToList();
					GET.Add(temp2[0], HttpUtility.UrlDecode(temp2[1]));
				}
			}

			// Приведение пути запроса к читабельному виду.
			// То есть обработка кодов по типу %20.
			Path = (Path.EndsWith("/") ? Path : Path + "/");
			Path = HttpUtility.UrlDecode(Path);
		}

    }
}
