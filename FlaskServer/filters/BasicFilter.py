from utils import convert_to_date


class Filter:
    def __init__(self, page: int = 1, page_size: int = 5, category: str = "", start_date: str = "", end_date: str = ""):
        self.page = page
        self.page_size = page_size
        self.category = category
        self.start_date = convert_to_date(start_date)
        self.end_date = convert_to_date(end_date)

    def run(self, notes):
        filtered_notes = notes

        if self.category != '':
            filtered_notes = [note for note in filtered_notes if self.category in [category.name for category in note.categories]]

        if self.start_date != '':
            filtered_notes = [note for note in filtered_notes if note.date.date() >= self.start_date]

        if self.end_date != '':
            filtered_notes = [note for note in filtered_notes if note.date.date() <= self.end_date]

        notes_size = len(filtered_notes)
        start = (self.page - 1) * self.page_size
        end = start + self.page_size if start + self.page_size < notes_size else notes_size
        return filtered_notes[start:end]
