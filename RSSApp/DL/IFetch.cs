namespace RSSApp {
    interface IFetch<T> {

        string FetchURL { get; set; }
        T Fetch();
    }
}

