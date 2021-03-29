using System.Collections.Generic;
using A8_MediaSearch.Models;

namespace A8_MediaSearch.Data
{
    interface IRepository
    {
        List<Media> getMediaList(int mediaCode);
        string getName();

        void viewAll(int mediaCode);

        void searchById(int mediaCode);

        void searchByTitle(int mediaCode);

        void searchByGenre(int mediaCode);

        void writeData(List<string> media, string strPath);

        int getLineNum(int mediaCode);
    }
}