const API = 'http://localhost:5150/api/restoration';
let editingId = null;

async function loadData() {
  const tbody = document.getElementById('data-body');
  tbody.innerHTML = '<tr class="loading-row"><td colspan="5">Đang tải...</td></tr>';
  try {
    const res = await fetch(API);
    const items = await res.json();
    if (items.length === 0) {
      tbody.innerHTML = '<tr class="empty-row"><td colspan="5">Chưa có dữ liệu</td></tr>';
      return;
    }
    tbody.innerHTML = items.map(item => `
      <tr>
        <td>${item.id}</td>
        <td>${item.date || ''}</td>
        <td>${item.description || ''}</td>
        <td>${item.artifactId}</td>
        <td class="actions-cell">
          <button class="btn btn-edit btn-sm" onclick='openEditModal(${JSON.stringify(item)})'>Sửa</button>
          <button class="btn btn-danger btn-sm" onclick="deleteItem(${item.id})">Xóa</button>
        </td>
      </tr>
    `).join('');
  } catch (err) {
    tbody.innerHTML = '<tr class="empty-row"><td colspan="5">Lỗi kết nối API</td></tr>';
  }
}

function openAddModal() {
  editingId = null;
  document.getElementById('modal-title').textContent = 'Thêm phục chế';
  document.getElementById('form-date').value = '';
  document.getElementById('form-description').value = '';
  document.getElementById('form-artifactId').value = '';
  document.getElementById('modal').classList.add('show');
}

function openEditModal(item) {
  editingId = item.id;
  document.getElementById('modal-title').textContent = 'Sửa phục chế';
  document.getElementById('form-date').value = item.date || '';
  document.getElementById('form-description').value = item.description || '';
  document.getElementById('form-artifactId').value = item.artifactId || '';
  document.getElementById('modal').classList.add('show');
}

function closeModal() {
  document.getElementById('modal').classList.remove('show');
}

async function saveItem() {
  const body = {
    date: document.getElementById('form-date').value,
    description: document.getElementById('form-description').value,
    artifactId: parseInt(document.getElementById('form-artifactId').value) || 0
  };

  try {
    if (editingId) {
      body.id = editingId;
      await fetch(`${API}/${editingId}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(body)
      });
    } else {
      await fetch(API, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(body)
      });
    }
    closeModal();
    loadData();
  } catch (err) {
    alert('Lỗi: ' + err.message);
  }
}

async function deleteItem(id) {
  if (!confirm('Bạn có chắc muốn xóa?')) return;
  try {
    await fetch(`${API}/${id}`, { method: 'DELETE' });
    loadData();
  } catch (err) {
    alert('Lỗi: ' + err.message);
  }
}

loadData();
