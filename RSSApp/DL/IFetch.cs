using System;

namespace RSSApp.DL {
    interface IFetch<T> {

        Uri FetchURL { get; set; }
        T Fetch();
    }
}

