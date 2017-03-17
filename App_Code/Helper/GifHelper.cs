using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Gif.Components;  //外部Gif.Components.dll
using System.Drawing;
using System.Drawing.Imaging;

/// <summary>
/// GifHelper 的摘要说明
/// </summary>
public class GifHelper
{
    public GifHelper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// 合併GIF
    /// </summary>
    /// <param name="savePath">保存的地址</param>
    /// <param name="imgList">圖片集</param>
    /// <param name="delayList">延遲時間集</param>
    /// <returns></returns>
    public static string GetThumbnail(string savePath, List<Image> imgList, List<int> delayList)
    {
        try
        {
            if (imgList.Count == delayList.Count)
            {
                string saveImg = savePath + Guid.NewGuid().ToString() + ".gif";
                string outPutPath = HttpContext.Current.Server.MapPath(saveImg); // 生成保存圖片的名稱
                AnimatedGifEncoder animate = new AnimatedGifEncoder();
                animate.Start(outPutPath);
                animate.SetRepeat(0); // -1：不重複，0：循環
                var imgCount = imgList.Count;
                for (int i = 0; i < imgCount; i++)
                {
                    animate.SetDelay(delayList[i]);  //時間
                    animate.AddFrame(imgList[i]); // 添加帧
                }
                animate.Finish();
                return saveImg;
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        return null;
    }

    /// <summary>
    /// 分解GIF
    /// </summary>
    /// <param name="pPath">需要分解的圖片</param>
    /// <param name="delayList">延遲時間集</param>
    /// <returns></returns>
    public static List<Image> GetFrames(string pPath,out List<int> delayList)
    {
        Image gif = Image.FromFile(pPath);
        List<int> delays = new List<int>();
        FrameDimension fd = new FrameDimension(gif.FrameDimensionsList[0]); //獲取帧數
        int count = gif.GetFrameCount(fd);
        List<Image> gifList = new List<Image>();  //保存帧
        for (int i = 0; i < count; i++)
        {
            gif.SelectActiveFrame(fd, i);
            if (i == 0)
            {
                for (int j = 0; j < gif.PropertyIdList.Length; j++)//遍歷屬性
                {
                    if ((int)gif.PropertyIdList.GetValue(j) == 0x5100)//.如果是延時時間
                    {
                        PropertyItem pItem = (PropertyItem)gif.PropertyItems.GetValue(j);//獲取延時時間屬性
                        byte[] delayByte = new byte[4];//延時時間
                        delayByte[0] = pItem.Value[i * 4];
                        delayByte[1] = pItem.Value[1 + i * 4];
                        delayByte[2] = pItem.Value[2 + i * 4];
                        delayByte[3] = pItem.Value[3 + i * 4];
                        delays.Add(BitConverter.ToInt32(delayByte, 0) * 10); //*10，獲取毫秒
                        break;
                    }
                }
            }
            gifList.Add(new Bitmap(gif));
        }
        gif.Dispose();
        delayList = delays;
        return gifList;
    }
}