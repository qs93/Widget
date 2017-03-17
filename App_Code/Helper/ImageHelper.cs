using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Drawing.Imaging;
using System.Drawing;
using System.IO;

/// <summary>
/// ImageHelper 的摘要说明
/// </summary>
public class ImageHelper
{
    public ImageHelper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    private static ImageCodecInfo GetEncoderInfo(String mimeType)
    {
        int j;
        ImageCodecInfo[] encoders;
        encoders = ImageCodecInfo.GetImageEncoders();
        for (j = 0; j < encoders.Length; ++j)
        {
            if (encoders[j].MimeType == mimeType)
                return encoders[j];
        }
        return null;
    }

    /// <summary>
    /// 圖片壓縮(降低質量以減少文件的大小)
    /// </summary>
    /// <param name="srcBitmap">傳入的Bitmap對象</param>
    /// <param name="destStream">壓縮後的Stream對象</param>
    /// <param name="level">壓縮等級，0到100，0 最差質量，100 最佳</param>
    public static void Compress(Bitmap srcBitmap, Stream destStream, long level)
    {
        ImageCodecInfo myImageCodecInfo;
        Encoder myEncoder;
        EncoderParameter myEncoderParameter;
        EncoderParameters myEncoderParameters;
        
        myImageCodecInfo = GetEncoderInfo("image/jpeg");
        myEncoder = Encoder.Quality;
        myEncoderParameters = new EncoderParameters(1);
        myEncoderParameter = new EncoderParameter(myEncoder, level);
        myEncoderParameters.Param[0] = myEncoderParameter;
        srcBitmap.Save(destStream, myImageCodecInfo, myEncoderParameters);
    }

    /// <summary>
    /// 圖片壓縮(降低質量以減少文件的大小)
    /// </summary>
    /// <param name="srcBitMap">傳入的Bitmap對象</param>
    /// <param name="destFile">壓縮後的圖片保存路徑</param>
    /// <param name="level">壓縮等級，0到100，0 最差質量，100 最佳</param>
    public static void Compress(Bitmap srcBitMap, string destFile, long level)
    {
        Stream s = new FileStream(destFile, FileMode.Create);
        Compress(srcBitMap, s, level);
        s.Close();
    }
}