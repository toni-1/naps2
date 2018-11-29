﻿using System;
using System.Collections.Generic;
using System.Linq;
using NAPS2.Recovery;
using NAPS2.Scan.Images.Transforms;

namespace NAPS2.Scan.Images.Storage
{
    public class RecoverableImageMetadata : IImageMetadata
    {
        private readonly RecoveryStorageManager rsm;
        private RecoveryIndexImage indexImage;

        public RecoverableImageMetadata(RecoveryStorageManager rsm, RecoveryIndexImage indexImage)
        {
            this.rsm = rsm;
            // TODO: Maybe not a constructor param?
            this.indexImage = indexImage;
        }

        public List<Transform> TransformList
        {
            get => indexImage.TransformList;
            set => indexImage.TransformList = value;
        }

        public int Index
        {
            get => rsm.Index.Images.IndexOf(indexImage);
            set
            {
                // TODO: Locking
                rsm.Index.Images.Remove(indexImage);
                rsm.Index.Images.Insert(value, indexImage);
            }
        }

        public ScanBitDepth BitDepth
        {
            get => indexImage.BitDepth;
            set => indexImage.BitDepth = value;
        }

        public bool Lossless
        {
            get => indexImage.HighQuality;
            set => indexImage.HighQuality = value;
        }

        public void Commit()
        {
            rsm.Commit();
        }

        public bool CanSerialize => true;

        public byte[] Serialize(IStorage storage)
        {
            throw new NotImplementedException();
        }

        public IStorage Deserialize(byte[] serializedData)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            rsm.Index.Images.Remove(indexImage);
            // TODO: Commit?
        }
    }
}
