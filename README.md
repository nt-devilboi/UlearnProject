# РЕГЛАМЕНТ XD
## ССЫЛКИ ПО ПРОЕКТУ
[Веб](https://github.com/K0ch3rga/ToDo-Timer) \
\
Слои:\
\
[Контроллеры](https://github.com/nt-devilboi/UlearnProject/tree/dev/Controllers) 

[Репозитории](https://github.com/nt-devilboi/UlearnProject/tree/master/Infrasturcture/Repositories) 

[DI контейнер](https://github.com/nt-devilboi/UlearnProject/blob/master/Program.cs)

[Domain](https://github.com/nt-devilboi/UlearnProject/tree/master/Domen/Entities)

## КРАТКОЕ ОПИСАНИЕ

МЫ роботали над интерактивыным ежедневником с возможностью отслеживания затраченного на на свои дела времени, его анализа и возможной оптимизации в дальнейшем. 
Проект выполнен с помощью Framework-a ASP.NET Core, фронтенд написан на REACT. Контроллеры принимают API запросы по ссылке, после чего запрашивают у репозитория необходимые данные.
Репозиторий взаимодействует напрямую с БД. Также присутствует возможность авторизоваться по протоколу OAuth 2.0. Можно авторизоваться через ВК, Гит и Гугл акк. 
Фронт запрашивает ссылки авторизации у бэкенда. Переходя по одной из ссылок авторизации мы попадаем на сервис. После авторизации мы переходим на бэкенд с тоекном сессии, после чего на фронте мы можем обращаться к бэкенду с токеном авторизации,
чтобы подтвердить нашу личностб. После переходы в приложение мы попадаем в наш ежедневник, где можно удобно распланироват свой день, создать дела в календаре, отследить их продолжительность,
оставить необходимые пометки на будущее, составлять списки дел, чек литсы. 

