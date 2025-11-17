namespace GuessIt.Services;

public class UploadFileService
{
    public string UploadImageAsync(IFormFile file)
    {
        List<string> validExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".gif" };
        var extension = Path.GetExtension(file.FileName).ToLower();
        if (!validExtensions.Contains(extension))
        {
            throw new Exception("Invalid file type. Only image files are allowed.");
        }

        long size = file.Length;
        if(size > (5 * 1024 * 1024))
        {
            throw new Exception("File size exceeds the 5MB limit.");
        }
        string fileName = Guid.NewGuid().ToString() + extension;
        string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
        FileStream fs = new FileStream(Path.Combine(path, fileName), FileMode.Create);
        file.CopyTo(fs);
        fs.Dispose();
        fs.Close();
        
        return fileName;
    }
}