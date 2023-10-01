namespace Fs;

public static class MimeHelper
{
    public static string? GetMimeType(string fileName)
    {
        string? mimeType = null;

        if (fileName.Contains(".png"))
        {
            mimeType = "image/png";
        } 
        else if (fileName.Contains(".jpg"))
        {
            mimeType = "image/jpg";
        }
        else if (fileName.Contains(".jpeg"))
        {
            mimeType = "image/jpeg";
        } 
        else if (fileName.Contains(".gif"))
        {
            mimeType = "image/gif";
        }
        else if (fileName.Contains(".mp3"))
        {
            mimeType = "audio/mpeg";
        }
        else if (fileName.Contains(".wav"))
        {
            mimeType = "audio/wav";
        }
        else if (fileName.Contains(".rar"))
        {
            mimeType = "application/octet-stream";
        }
        else if (fileName.Contains(".zip"))
        {
            mimeType = "application/x-zip-compressed";
        }
        else if (fileName.Contains(".exe"))
        {
            mimeType = "application/octet-stream";
        }
        else if (fileName.Contains(".svg"))
        {
            mimeType = "image/svg+xml";
        }
        else if (fileName.Contains(".doc"))
        {
            mimeType = "application/msword";
        }
        else if (fileName.Contains(".docx"))
        {
            mimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        }
        else if (fileName.Contains(".xls"))
        {
            mimeType = "application/vnd.ms-excel";
        }
        else if (fileName.Contains(".pdf"))
        {
            mimeType = "application/pdf";
        }
        else if (fileName.Contains(".txt"))
        {
            mimeType = "text/plain";
        }
        else if (fileName.Contains(".7z"))
        {
            mimeType = "application/x-7z-compressed";
        }
        return mimeType;
    }
}