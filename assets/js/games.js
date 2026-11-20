const searchInput =
    document.getElementById("search");

searchInput?.addEventListener(
    "input",
    function () {
        const search =
            this.value.toLowerCase();

        const cards =
            document.querySelectorAll(".game-card");

        cards.forEach(card => {
            const name =
                card.dataset.name;

            card.style.display =
                name.includes(search)
                    ? ""
                    : "none";
        });
    });

const sortSelect =
    document.getElementById("sort");

sortSelect?.addEventListener(
    "change",
    function () {

        const grid =
            document.getElementById("games");

        const cards =
            Array.from(
                document.querySelectorAll(
                    ".game-card"
                )
            );

        switch (this.value)
        {
            case "rating-desc":
                cards.sort((a, b) =>
                    Number(b.dataset.rating) -
                    Number(a.dataset.rating));
                break;

            case "rating-asc":
                cards.sort((a, b) =>
                    Number(a.dataset.rating) -
                    Number(b.dataset.rating));
                break;

            case "name-asc":
                cards.sort((a, b) =>
                    a.dataset.name.localeCompare(
                        b.dataset.name));
                break;

            case "name-desc":
                cards.sort((a, b) =>
                    b.dataset.name.localeCompare(
                        a.dataset.name));
                break;
        }

        cards.forEach(card =>
            grid.appendChild(card));
    });