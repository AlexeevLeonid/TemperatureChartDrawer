# TemperatureChartDrawer

Приложение собирает числовой показатель с сайта, если страницу можно получить обычным get-запросом, раз в указанный промежуток времени.
Перед собираем значением должна быть любая уникальная неизменяемая строка(подойдёт html-тег).
В текущей конфигурации интервал между записями 10000 мс, редактируется в appsettings.json

Состоит из 3 страниц

На главной странице присутствует ссылка список источников: название, сайт, теги, обрамляющие собираемое значение, период, кнопки для просмотра графика и для удаления источника

<img src="https://user-images.githubusercontent.com/79642783/231136842-ca3c7dd9-0a79-44b9-8ee4-5f0929683419.png" width="600">

Страница формы добавления нового источника

<img src="https://github.com/AlexeevLeonid/TemperatureChartDrawer/assets/79642783/b973e37d-4379-44bd-bc5a-23c00a0db123" width="600">

Страница данных (скачки - приложение выключалось, и не записывало данные)

<img src="https://user-images.githubusercontent.com/79642783/231137088-f5ab9273-cd5a-445e-b944-70a42db22636.png" width="600">

