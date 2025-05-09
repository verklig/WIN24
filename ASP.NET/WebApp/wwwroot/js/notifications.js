const connection = new signalR.HubConnectionBuilder().withUrl("/notificationHub", {withCredentials: true}).build();

updateNotificationCount();
updateRelativeTimes();
setInterval(updateRelativeTimes, 60000);

connection.on("AllReceiveNotification", function(notification) {
  const container = document.querySelector('.notification-container');

  const item = document.createElement('div');
  item.className = 'notification-card';
  item.setAttribute('data-id', notification.id);
  item.innerHTML = `
    <img src="${notification.image}" alt="Notification Image" class="image">
    <div>
      <h3 class="message">${notification.message}</h3>
      <span class="time" data-created="${new Date(notification.created).toISOString()}">${notification.created}</span>
    </div>
    <button class="btn" onclick="dismissNotification('${notification.id}')"><i class="fa-regular fa-xmark"></i></button>
  `;

  if (!container.querySelector(`.notification-card[data-id="${notification.id}"]`)) {
    container.insertBefore(item, container.firstChild);
  }

  updateRelativeTimes();
  updateNotificationCount();
});

connection.on("NotificationDismissed", function (notificationId) {
  removeNotification(notificationId);
});

connection.start().catch(error => console.error(error));

async function dismissNotification(notificationId) {
  try {
    const res = await fetch(`/api/notifications/dismiss/${notificationId}`, { method : 'POST' })
    if (res.ok) {
      removeNotification(notificationId);
    }
    else {
      console.error('Error removing notification.');
    }
  }
  catch (error) {
    console.error('Error removing notification: ', error)
  }
}

function removeNotification(notificationId) {
  const element = document.querySelector(`.notification-card[data-id="${notificationId}"]`);

  if (element) {
    element.remove();
    updateNotificationCount();
  }
}

function updateNotificationCount() {
  const notificationDot = document.querySelector('.notification-dot');
  const notificationCounter = document.querySelector('.notification-counter');
  const bellDefault = document.querySelector('.bell-default');
  const bellActiveLight = document.querySelector('.bell-active-light');
  const bellActiveDark = document.querySelector('.bell-active-dark');
  const noNotificationsMsg = document.querySelector('.no-notifications');
  const isDark = document.documentElement.getAttribute('data-theme') === 'dark';
  const count = document.querySelectorAll('.notification-card').length;

  if (notificationCounter) {
    notificationCounter.textContent = count;
  }

  if (count > 0) {
    bellDefault?.classList.add('hidden');
    bellActiveLight?.classList.toggle('hidden', isDark);
    bellActiveDark?.classList.toggle('hidden', !isDark);
    noNotificationsMsg?.classList.add('hidden');
    notificationDot?.classList.remove('hidden');
  } else {
    bellDefault?.classList.remove('hidden');
    bellActiveLight?.classList.add('hidden');
    bellActiveDark?.classList.add('hidden');
    noNotificationsMsg?.classList.remove('hidden');
    notificationDot?.classList.add('hidden');
  }
}

function updateRelativeTimes() {
  const now = new Date();

  document.querySelectorAll('.notification-card .time').forEach(span => {
    const created = new Date(span.getAttribute('data-created'));
    const diffMs = now - created;
    const diffMins = Math.floor(diffMs / 60000);

    let text;

    if (diffMins < 1) {
      text = "Just now";
    } else if (diffMins < 60) {
      text = `${diffMins} ${diffMins === 1 ? "min" : "mins"} ago`;
    } else {
      const diffHrs = Math.floor(diffMins / 60);
      if (diffHrs < 24) {
        text = `${diffHrs} ${diffHrs === 1 ? "hour" : "hours"} ago`;
      } else {
        const diffDays = Math.floor(diffHrs / 24);
        if (diffDays < 7) {
          text = `${diffDays} ${diffDays === 1 ? "day" : "days"} ago`;
        } else if (diffDays < 30) {
          const weeks = Math.floor(diffDays / 7);
          text = `${weeks} ${weeks === 1 ? "week" : "weeks"} ago`;
        } else if (diffDays < 365) {
          const months = Math.floor(diffDays / 30);
          text = `${months} ${months === 1 ? "month" : "months"} ago`;
        } else {
          const years = Math.floor(diffDays / 365);
          text = `${years} ${years === 1 ? "year" : "years"} ago`;
        }
      }
    }

    span.textContent = text;
  });
}