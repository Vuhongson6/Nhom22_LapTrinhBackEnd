const API = 'http://localhost:5150/api/exhibition';
let editingId = null;

async function loadData() {
  const tbody = document.getElementById('data-body');
  tbody.innerHTML = '<tr class="loading-row"><td colspan="6">Đang tải...</td></tr>';
  try {
    const res = await fetch(API);
    const items = await res.json();
    if (items.length === 0) {
      tbody.innerHTML = '<tr class="empty-row"><td colspan="6">Chưa có dữ liệu</td></tr>';
      return;
    }
    tbody.innerHTML = items.map(item => `
      <tr>
        <td>${item.id}</td>
        <td>${item.name || ''}</td>
        <td>${item.description || ''}</td>
        <td>${item.startDate || ''}</td>
        <td>${item.endDate || ''}</td>
        <td class="actions-cell">
          <button class="btn btn-edit btn-sm" onclick='openEditModal(${JSON.stringify(item)})'>Sửa</button>
          <button class="btn btn-danger btn-sm" onclick="deleteItem(${item.id})">Xóa</button>
        </td>
      </tr>
    `).join('');
  } catch (err) {
    tbody.innerHTML = '<tr class="empty-row"><td colspan="6">Lỗi kết nối API</td></tr>';
  }
}

function openAddModal() {
  editingId = null;
  document.getElementById('modal-title').textContent = 'Thêm triển lãm';
  document.getElementById('form-name').value = '';
  document.getElementById('form-description').value = '';
  document.getElementById('form-startDate').value = '';
  document.getElementById('form-endDate').value = '';
  document.getElementById('modal').classList.add('show');
}

function openEditModal(item) {
  editingId = item.id;
  document.getElementById('modal-title').textContent = 'Sửa triển lãm';
  document.getElementById('form-name').value = item.name || '';
  document.getElementById('form-description').value = item.description || '';
  document.getElementById('form-startDate').value = item.startDate || '';
  document.getElementById('form-endDate').value = item.endDate || '';
  document.getElementById('modal').classList.add('show');
}

function closeModal() {
  document.getElementById('modal').classList.remove('show');
}

async function saveItem() {
  const body = {
    name: document.getElementById('form-name').value,
    description: document.getElementById('form-description').value,
    startDate: document.getElementById('form-startDate').value,
    endDate: document.getElementById('form-endDate').value
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
