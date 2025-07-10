const apiBase = "https://localhost:7126"; // замени на свой, если нужно

// Загрузка слотов
async function loadSlots() {
    const res = await fetch(`${apiBase}/api/slot`);
    const slots = await res.json();
    const list = document.getElementById("slotList");

    slots.forEach(slot => {
        const date = new Date(slot.startTime);
        const full = slot.booked >= slot.maxClients;
        const free = slot.maxClients - slot.booked;

        const col = document.createElement("div");
        col.className = "col-md-6 col-lg-4";

        col.innerHTML = `
            <div class="card shadow-sm h-100">
                <div class="card-body">
                    <h5 class="card-title">${date.toLocaleString("ru-RU")}</h5>
                    <p class="card-text">
                        Свободных мест: <strong>${free}</strong> / ${slot.maxClients}
                    </p>
                    ${!full
                        ? `<a href="booking.html?slotId=${slot.id}" class="btn btn-success w-100">Записаться</a>`
                        : `<button class="btn btn-secondary w-100" disabled>Мест нет</button>`}
                </div>
            </div>
        `;
        list.appendChild(col);
    });
}

// Отправка записи
async function sendBooking(data) {
    try {
        const res = await fetch(`${apiBase}/api/booking`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(data)
        });

        if (res.ok) return true;
        const error = await res.text();
        return `❌ Ошибка: ${error}`;
    } catch (err) {
        return "⚠️ Не удалось подключиться к серверу.";
    }
}
