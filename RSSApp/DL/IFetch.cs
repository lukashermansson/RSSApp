using System;

namespace RSSApp {
    interface IFetch<T> {

        Uri FetchURL { get; set; }
        T Fetch();
    }
}

