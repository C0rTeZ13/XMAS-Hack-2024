import pandas as pd
import numpy as np

pd.set_option('display.max_columns', None)

del_props = ["ИНН", "Дата регистрации", "Адрес", "ФИО Генерального директора", "Дата рождения Генерального директора", "ФИО Бенефициара",
             "Сайт", "Номер телефона", "Провайдер", "Деятельность клиента со слов клиента", "Кол-во сотрудников со слов клиента",
             " Срок жизни SIM-карты/номера (от даты замены e/SIM-карты)", "Срок жизни SIM в текущем пользовательском устройстве",
             "Срок жизни SIM-карты/номера (количество дней/часов/минут, которое прошло от даты заключения договора)"]

data = pd.read_csv("data.csv")
# data = pd.read_excel("data.xlsx")
data = data.drop(columns=del_props)

data["Вся негативная информация"] = data["Негативная информация"].combine_first(data["Негатив относительно ГД"])
data = data.drop(columns=["Негативная информация", "Негатив относительно ГД"])

str_categories = pd.DataFrame()
for column in data.columns:
    if data[column].dtype == "object":
        str_categories[column] = data[column]
        data[column] = data[column].astype("category").cat.codes

data["ЗСК"] = data["ЗСК"].fillna(0)

weights = np.random.rand(1, len(data.columns))
weights_categories = pd.DataFrame(weights, columns=data.columns.tolist())

data["score"] = 0.0
for index, row in data.iterrows():
    w = 0.0
    score = 0.0
    for column in data.columns:
        if column != "score" and pd.notna(row[column]) and row[column] != -1:
            w += weights_categories[column].item()
            score += weights_categories[column].item() * row[column].item()

    if w > 0:
        data.loc[index, "score"] = score / w

    if index % 10000 == 0:
        print(index)

print(data)
weights_categories.to_csv('weights.csv', index=False)
data.to_csv('output.csv', index=False)
