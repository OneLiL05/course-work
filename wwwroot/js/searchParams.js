class SearchParams {
    #url = new URL(window.location.href)

    get(key) {
        return this.#url.searchParams.get(key)
    }

    set(key, value) {
        this.#url.searchParams.set(key, value)
    }

    handle(key, value) {
        if (value.length > 0) {
            this.#url.searchParams.set("search", value);
        } else {
            this.#url.searchParams.delete("search");
        }

        history.replaceState(history.state, "", this.#url.href);
    }

    delete(key) {
        this.#url.searchParams.delete(key);
    }
}
