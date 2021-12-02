namespace AoC2021.Common
{
    public class AoCClient
    {
        private readonly HttpClient _httpClient = new();
        private readonly string _sessionToken;

        public AoCClient(string sessionToken)
        {
            _sessionToken = sessionToken;
        }

        public async Task<string> GetInput(int day, int year)
        {
            var result = await GetCachedAsync(day, year);
            if (result.IsNullOrEmpty())
            {
                result = await GetFromWebsiteAsync(day, year);
                await PutCacheAsync(day, year, result);
            }
            return result ?? string.Empty;
        }

        private async Task<string> GetFromWebsiteAsync(int day, int year)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, $"https://adventofcode.com/{year}/day/{day}/input");
            request.Headers.TryAddWithoutValidation("cookie", $"session={_sessionToken}");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        private async Task PutCacheAsync(int day, int year, string result)
        {
            var file = GetFileForDay(day, year);
            await file.WriteAllTextAsync(result);
        }

        private async Task<string> GetCachedAsync(int day, int year)
        {
            var file = GetFileForDay(day, year);
            return file.Exists ? await file.ReadAllTextAsync() : string.Empty;
        }

        private static FileInfo GetFileForDay(int day, int year)
        {
            var dir = Directory.CreateDirectory("Cache");
            var file = dir.GetFileInfo($"{year}_{day}.txt");
            return file;
        }
    }

    public static class Extensions
    {
        public static bool IsNullOrEmpty(this string input)
        {
            return string.IsNullOrEmpty(input);
        }

        public static async Task<string> ReadAllTextAsync(this FileInfo fileInfo)
        {
            if (!fileInfo.Exists)
            {
                return string.Empty;
            }

            try
            {
                return await File.ReadAllTextAsync(fileInfo.FullName);
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        public static async Task<FileInfo> WriteAllTextAsync(this FileInfo fileInfo, string content)
        {
            await File.WriteAllTextAsync(fileInfo.FullName, content);
            return fileInfo;
        }

        public static FileInfo GetFileInfo(this DirectoryInfo directoryInfo, string fileName)
        {
            return new FileInfo(Path.Combine(directoryInfo.FullName, fileName));
        }

        public static IEnumerable<string> GetLines(this string str, bool removeEmptyLines = false)
        {
            using (var sr = new StringReader(str))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (removeEmptyLines && string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }
                    yield return line;
                }
            }
        }
    }
}