class Filter {
    constructor(page, pageSize, category, startDate, endDate) {
        this.page = parseInt(page);
        this.pageSize = parseInt(pageSize);
        this.category = category;
        this.startDate = startDate !== undefined ? new Date(startDate) : startDate;
        this.endDate = endDate !== undefined ? new Date(endDate) : endDate;
    }
}

class Note {
    constructor(title, description, isMarkdownFile, date, categories) {
        this.title = title;
        this.description = description;
        this.isMarkdownFile = isMarkdownFile;
        this.date = new Date(date);
        this.formattedDate = this.date.getFullYear() + "/" + (this.date.getMonth() + 1) + "/" + this.date.getDate();
        this.categories = categories;
    }

    static createFromJson = function (jsonObject) {
        return new Note(jsonObject.title, jsonObject.description, jsonObject.isMarkdownFile, jsonObject.date, jsonObject.categories);
    };
}

module.exports = {
    Filter: Filter,
    Note: Note
}; 
