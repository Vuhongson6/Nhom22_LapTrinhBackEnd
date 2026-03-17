const API = 'http://localhost:5150/api/statistics/dashboard';

async function loadDashboard() {
  try {
    const res = await fetch(API);
    const data = await res.json();
    document.getElementById('artifact-count').textContent = data.artifacts;
    document.getElementById('exhibition-count').textContent = data.exhibitions;
    document.getElementById('staff-count').textContent = data.staff;
    document.getElementById('restoration-count').textContent = data.restorations;
  } catch (err) {
    console.error('Lỗi tải dashboard:', err);
    document.getElementById('artifact-count').textContent = '!';
    document.getElementById('exhibition-count').textContent = '!';
    document.getElementById('staff-count').textContent = '!';
    document.getElementById('restoration-count').textContent = '!';
  }
}

loadDashboard();
