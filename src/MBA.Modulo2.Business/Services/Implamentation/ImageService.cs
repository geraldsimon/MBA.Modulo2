using MBA.Modulo2.Business.Services.Interface;
using Microsoft.AspNetCore.Http;

namespace MBA.Modulo2.Business.Services.Implamentation;

public class ImageService : IImageService
{
    public readonly string ImageLibraryPath;

    public ImageService()
    {
        // Get the path of the WebApp project
        var webAppPath = Directory.GetCurrentDirectory();

        // Go back one level (to the "Solution" folder) and access the shared project folder
        var assemblyLocation = Path.Combine(webAppPath, "..", "MBA.Modulo2.App", "wwwroot\\Images");

        // Normalizes the absolute path
        ImageLibraryPath = Path.GetFullPath(assemblyLocation);


        if (!Directory.Exists(ImageLibraryPath))
        {
            Directory.CreateDirectory(ImageLibraryPath);
        }
    }

    public async Task<string> SaveImageAsync(IFormFile file, string fileName)
    {
        if (file == null || file.Length == 0)
        {
            return null;
        }

        string filePath = Path.Combine(ImageLibraryPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return filePath; 
    }

    public async Task<string> SaveImageAsync(string file, string fileName)
    {
        if (file == null || file.Length == 0)
        {
            return null;
        }
        string extension = GetExtensionFromBase64(file);
        string completeFileName = $"{fileName}{extension}";
        var formFile = ConvertBase64ToIFormFile(file, fileName, $"wwwroot//image/{extension.Replace(".", "")}");
        string filePath = Path.Combine(ImageLibraryPath, completeFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await formFile.CopyToAsync(stream);
        }

        return completeFileName; 
    }

    public async Task<string> UpdateImageAsync(string file, string fileName, string oldFileName)
    {
        if (file == null || file.Length == 0)
        {
            return null;
        }

        var completeName = $"{Guid.NewGuid()}_{fileName}".Trim();

        string extension = GetExtensionFromBase64(file);
        string completeFileName = $"{completeName}{extension}";
        var formFile = ConvertBase64ToIFormFile(file, completeName, $"wwwroot//image/{extension.Replace(".", "")}");
        string filePath = Path.Combine(ImageLibraryPath, completeFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await formFile.CopyToAsync(stream);
        }


        return completeFileName;
    }
    
    public bool DeleteFile( string fileName)
    {
        string filePathBame = $"{ImageLibraryPath}\\{fileName}";
        if (File.Exists(filePathBame))
        {
            File.Delete(filePathBame);
        }
       return true;
    }

    public string ConvertImageToBase64(string image)
    {
        var completePath = $"{ImageLibraryPath}\\{image}";
        var extension = GetExtensionFromBase64(image);

        if (!File.Exists(completePath))
            throw new FileNotFoundException("Image file not found.", image);

        byte[] imageBytes = File.ReadAllBytes(completePath); 
        var base64 = Convert.ToBase64String(imageBytes); 

        var finalString = $"data:image/{extension};base64,{base64}";

        return finalString;
    }

    private static IFormFile ConvertBase64ToIFormFile(string base64String, string fileName, string contentType)
    {
        base64String = RemoveBase64Prefix(base64String);

        // Remover o prefixo (caso exista) do Base64 (ex: "data:image/png;base64,")
        var base64Data = base64String.Contains(',') ? base64String.Split(',')[1] : base64String;

        // Converter Base64 para array de bytes
        byte[] fileBytes = Convert.FromBase64String(base64Data);

        // Criar um Stream a partir dos bytes
        var stream = new MemoryStream(fileBytes);

        // Criar um IFormFile a partir do Stream
        return new FormFile(stream, 0, fileBytes.Length, "file", fileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = contentType
        };
    }
  
    public static string GetExtensionFromBase64(string base64String)
    {
        // Extrair o MIME Type (caso a string tenha um prefixo)
        if (base64String.Contains(","))
            base64String = base64String.Split(',')[0];

        return base64String switch
        {
            "data:image/jpeg;base64" => ".jpg",
            "data:image/png;base64" => ".png",
            "data:image/gif;base64" => ".gif",
            "data:image/bmp;base64" => ".bmp",
            "data:image/webp;base64" => ".webp",
            "data:application/pdf;base64" => ".pdf",
            "data:text/plain;base64" => ".txt",
            "data:application/msword;base64" => ".doc",
            "data:application/vnd.openxmlformats-officedocument.wordprocessingml.document;base64" => ".docx",
            "data:application/vnd.ms-excel;base64" => ".xls",
            "data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64" => ".xlsx",
            _ => string.Empty // Retorna vazio se não conseguir identificar
        };
    }

    public static string RemoveBase64Prefix(string base64String)
    {
        if (base64String.Contains(','))
            return base64String.Split(',')[1]; 

        return base64String; 
    }
}