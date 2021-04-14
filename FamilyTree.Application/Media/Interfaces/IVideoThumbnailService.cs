namespace FamilyTree.Application.Media.Interfaces
{
    public interface IVideoThumbnailService
    {
        byte[] GetVideoThumbnailBytes(string videoFilePath);
    }
}
