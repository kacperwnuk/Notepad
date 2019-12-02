class Filter {
    constructor(page, pageSize, category, startDate, endDate) {
        this.page = parseInt(page);
        this.pageSize = parseInt(pageSize);
        this.category = category;
        this.startDate = startDate !== "" ? new Date(startDate) : startDate;
        this.endDate = endDate !== "" ? new Date(endDate) : endDate;
    }
}

class Note {
    constructor(title, description, isMarkdownFile, date, categories) {
        this.title = title;
        this.description = description;
        this.isMarkdownFile = isMarkdownFile;
        this.date = new Date(date);
        this.categories = categories;
    }

    static createFromJson = function (jsonObject) {
        if(jsonObject.title === undefined){
            throw new Error('Title can`t be undefined!');
        }
        return new Note(jsonObject.title, jsonObject.description, jsonObject.isMarkdownFile, jsonObject.date, jsonObject.categories);
    };
}

module.exports = {
    Filter: Filter,
    Note: Note
}; 
