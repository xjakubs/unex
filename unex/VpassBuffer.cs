using System.Collections.Generic;
using VpassConnection;

/**
 * Universal program for VIDA! science centrum for exhibits
 *
 * @author Jakub Smid (xjakubs@gmail.com)
 */
namespace UnExponat {
    public class VpassBuffer {

        private static VpassBuffer sInstance = null;
        private static int mCount;
        private static List<VpassData> mDataList;

        public static VpassBuffer getInstance() {
            if (sInstance == null) {
                sInstance = new VpassBuffer();
            }
            return sInstance;
        }

        private VpassBuffer() {
            mDataList = new List<VpassData>();
        }

        public void setCount(int count) {
            mCount = count;
        }

        public int getCount() {
            return mCount;
        }

        public void reset() {
            mCount = 0;
            sInstance = null;
            if (mDataList != null && mDataList.Count > 0) {
                mDataList.Clear();
            }
        }

        public void addData(VpassData data) {
            if (mCount > 0) {
                mDataList.Add(data);
            } else {
                return;
            }
        }

        public List<VpassData> getData() {
            return mDataList;
        }
    }
}