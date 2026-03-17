const API = 'http://localhost:5150/api/artifact';
let editingId = null;

async function loadData() {
  const tbody = document.getElementById('data-body');
  tbody.innerHTML = '<tr class="loading-row"><td colspan="7">Đang tải...</td></tr>';
  try {
    const res = await fetch(API);
    const items = await res.json();
    if (items.length === 0) {
      tbody.innerHTML = '<tr class="empty-row"><td colspan="7">Chưa có dữ liệu</td></tr>';
      return;
    }
    tbody.innerHTML = items.map(item => `
      <tr>
        <td>${item.id}</td>
        <td>${item.name || ''}</td>
        <td>${item.description || ''}</td>
        <td>${item.location || ''}</td>
        <td>${item.status || ''}</td>
        <td>${item.exhibitionId}</td>
        <td class="actions-cell">
          <button class="btn btn-edit btn-sm" onclick='openEditModal(${JSON.stringify(item)})'>Sửa</button>
          <button class="btn btn-danger btn-sm" onclick="deleteItem(${item.id})">Xóa</button>
        </td>
      </tr>
    `).join('');
  } catch (err) {
    tbody.innerHTML = '<tr class="empty-row"><td colspan="7">Lỗi kết nối API</td></tr>';
  }
}

function openAddModal() {
  editingId = null;
  document.getElementById('modal-title').textContent = 'Thêm hiện vật';
  document.getElementById('form-name').value = '';
  document.getElementById('form-description').value = '';
  document.getElementById('form-location').value = '';
  document.getElementById('form-status').value = '';
  document.getElementById('form-exhibitionId').value = '';
  document.getElementById('modal').classList.add('show');
}

function openEditModal(item) {
  editingId = item.id;
  document.getElementById('modal-title').textContent = 'Sửa hiện vật';
  document.getElementById('form-name').value = item.name || '';
  document.getElementById('form-description').value = item.description || '';
  document.getElementById('form-location').value = item.location || '';
  document.getElementById('form-status').value = item.status || '';
  document.getElementById('form-exhibitionId').value = item.exhibitionId || '';
  document.getElementById('modal').classList.add('show');
}

function closeModal() {
  document.getElementById('modal').classList.remove('show');
}

async function saveItem() {
  const exhibitionIdValue = document.getElementById('form-exhibitionId').value;
  const body = {
    name: document.getElementById('form-name').value,
    description: document.getElementById('form-description').value,
    location: document.getElementById('form-location').value,
    status: document.getElementById('form-status').value,
    exhibitionId: exhibitionIdValue === '' ? null : parseInt(exhibitionIdValue)
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
