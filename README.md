# Реализация простого платформера

Данный проект является тестовым заданием.

## Задание

Реализовать простые механики платформера:

- Спавн валюты (яблоки), простые idle анимации и помещение её в счётчик игрока.
- Врагов, которые будут патрулировать местность и наносить урон игроку.
- Получение игроком урона, а также нанесение урона врагам путем прыжка на голову врагов.
- Система смена уровней (в игре 2 уровня)
- Простые ловушки при поадании в которые игрок умирает.
- Анимации для врагов и игрока.

Цель каждого уровня:
- Найти и победить всех врагов на уровне.

---

[Скачать билд с Google Drive](https://drive.google.com/drive/folders/144kkDLfMqIjxFpWKr5Qs-1tT2EjLNko_?usp=sharing)

---

## На что следует обратить внимание в игре:

Получение урона игроком от врагов, когда тот касается не их головы (Получаем урон только пока разбирмаемся с управлением): 
<p align="center">
  <img src="Simple-Platformer/Assets/PreviewF/TakeDamage.gif" alt="TakeDamage" />
</p>

Смерть игрока от ловушеки (Нужно быть аккуратнее):
<p align="center">
  <img src="Simple-Platformer/Assets/PreviewF/Dead.gif" alt="Dead" />
</p>

Сбор валюты игроком (А зачем?):
<p align="center">
  <img src="Simple-Platformer/Assets/PreviewF/TakeApple.gif" alt="TakeApple" />
</p>

Время мести (Избиение врага):
<p align="center">
  <img src="Simple-Platformer/Assets/PreviewF/GiveDamage.gif" alt="GiveDamage" />
</p>

Прыжок веры (Рискнули бы?):
<p align="center">
  <img height="412" width="342" src="Simple-Platformer/Assets/PreviewF/LeapOfJump.png" alt="LeapOfJump" />
</p>

Менее интрегующий вылет с карты с последующей смертью:
<p align="center">
  <img height="412" width="342" src="Simple-Platformer/Assets/PreviewF/DeadFromSpace.gif" alt="DeadFromSpace" />
</p>

### Используемые технологии

<div align="center">
  <img src="https://www.svgrepo.com/show/331626/unity.svg" height="40" alt="Unity logo" />
  <img width="12" />
  <img src="https://upload.wikimedia.org/wikipedia/commons/thumb/b/bd/Logo_C_sharp.svg/910px-Logo_C_sharp.svg.png" height="40" alt="C# logo" />
  <img width="12" />
  <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT2V4Em4MiwRBTOsNO4Jo-QzpmvnNjyesUAvg&s" height="40" alt="DOTween logo" />
  <img width="12" />
  <img src="https://upload.wikimedia.org/wikipedia/commons/thumb/3/33/Figma-logo.svg/1365px-Figma-logo.svg.png" height="40" alt="Figma logo" />
  <img width="12" />
  <img src="https://upload.wikimedia.org/wikipedia/commons/thumb/6/69/Logo_Aseprite.svg/1911px-Logo_Aseprite.svg.png" height="40" alt="Aseprite logo"  />
</div>

