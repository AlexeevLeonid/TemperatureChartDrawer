# TemperatureChartDrawer

Приложение собирает числовой показатель с сайта, если страницу можно получить обычным get-запросом, раз в указанный промежуток времени.
Перед собираем значением должна быть любая уникальная неизменяемая строка(подойдёт html-тег).
В текущей конфигурации интервал между записями 10000 мс, редактируется в appsettings.json

Состоит из 3 страниц

На главной странице присутствует ссылка список источников: название, сайт, теги, обрамляющие собираемое значение, период, кнопки для просмотра графика и для удаления источника

<img src="https://github.com/AlexeevLeonid/TemperatureChartDrawer/assets/79642783/4b4b95fc-c22e-4b54-a4d7-46fdbc626c97" width="600">

Страница формы добавления нового источника

<img src="https://github.com/AlexeevLeonid/TemperatureChartDrawer/assets/79642783/ce9157ed-bba9-46c8-b73b-d065b9cb763e" width="600">

Страница данных

<img src="https://github.com/AlexeevLeonid/TemperatureChartDrawer/assets/79642783/1251b031-ede6-4373-becf-2ba8e9cb15e2" width="600">

