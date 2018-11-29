﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NAPS2.ImportExport.Pdf;
using NAPS2.Scan.Images.Storage;
using NAPS2.Scan.Images.Transforms;

namespace NAPS2.Scan.Images
{
    public class ScannedImageRenderer
    {
        private readonly IPdfRenderer pdfRenderer;

        public ScannedImageRenderer(IPdfRenderer pdfRenderer)
        {
            this.pdfRenderer = pdfRenderer;
        }

        public async Task<IMemoryStorage> Render(ScannedImage image, int outputSize = 0)
        {
            using (var snapshot = image.Preserve())
            {
                return await Render(snapshot, outputSize);
            }
        }

        public async Task<IMemoryStorage> Render(ScannedImage.Snapshot snapshot, int outputSize = 0)
        {
            return await Task.Factory.StartNew(() =>
            {
                var storage = StorageManager.ConvertToMemory(snapshot.Source.BackingStorage, new StorageConvertParams());
                if (outputSize > 0)
                {
                    double scaleFactor = Math.Min(outputSize / (double)storage.Height, outputSize / (double)storage.Width);
                    storage = StorageManager.PerformTransform(storage, new ScaleTransform { ScaleFactor = scaleFactor });
                }
                return StorageManager.PerformAllTransforms(storage, snapshot.TransformList);
            });
        }

        public async Task<Stream> RenderToStream(ScannedImage image)
        {
            using (var snapshot = image.Preserve())
            {
                return await RenderToStream(snapshot);
            }
        }

        public async Task<Stream> RenderToStream(ScannedImage.Snapshot snapshot)
        {
            using (var transformed = await Render(snapshot))
            {
                var stream = new MemoryStream();
                var format = transformed.PixelFormat == PixelFormat.Format1bppIndexed
                    ? ImageFormat.Png
                    : snapshot.Source.FileFormat ?? (snapshot.Source.RecoveryIndexImage.HighQuality ? ImageFormat.Png : ImageFormat.Jpeg);
                transformed.Save(stream, format);
                return stream;
            }
        }
    }
}
