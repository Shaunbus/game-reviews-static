# Hey.MD

This is a hobby wobsite of my game reviews. Somewhere around 2013 I started an Excel spreadsheet where I scored videogames games. I decided to create a website around them. The scores are objective and correct.

## Tools

- Using standard [Jekyll Github Pages](https://docs.github.com/en/pages/setting-up-a-github-pages-site-with-jekyll/about-github-pages-and-jekyll)
- Created a C# that:
  1. Reads the exported .csv of my games,
  2. hits some endpoints to gather images and metadata,
  3. and creates a JSON file out of it for the site

## API'S

- Images pulled from - [SteamGrid](https://www.steamgriddb.com/)
- Metadata info pulled from - [RAWG](https://rawg.io/apidocs)

### TODO

#### Website

- [x] Read using RAWG id instead of Slug
- [x] Add Sorting and Filtering
- [x] Add Genres
- [ ] Add Publishers and Devlopers
- [ ] Add side bar with Top 10s

#### C# Tool

- [ ] Allow selection of image instead of grabbing the highest rated
- [ ] Allow selection of the correct game instead of grabbing first result as the RAWG search ain't it
