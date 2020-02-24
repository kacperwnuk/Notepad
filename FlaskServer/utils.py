from datetime import datetime


def convert_to_date(date: str):
    try:
        return datetime.strptime(date, '%Y-%m-%d').date()
    except ValueError:
        return ''
