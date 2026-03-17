const API = 'http://localhost:5150/api/staff';
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
        <td>${item.position || ''}</td>
        <td>${item.phone || ''}</td>
        <td>${item.email || ''}</td>
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
  document.getElementById('modal-title').textContent = 'Thêm nhân viên';
  document.getElementById('form-name').value = '';
  document.getElementById('form-position').value = '';
  document.getElementById('form-phone').value = '';
  document.getElementById('form-email').value = '';
  document.getElementById('modal').classList.add('show');
}

function openEditModal(item) {
  editingId = item.id;
  document.getElementById('modal-title').textContent = 'Sửa nhân viên';
  document.getElementById('form-name').value = item.name || '';
  document.getElementById('form-position').value = item.position || '';
  document.getElementById('form-phone').value = item.phone || '';
  document.getElementById('form-email').value = item.email || '';
  document.getElementById('modal').classList.add('show');
}

function closeModal() {
  document.getElementById('modal').classList.remove('show');
}

async function saveItem() {
  const body = {
    name: document.getElementById('form-name').value,
    position: document.getElementById('form-position').value,
    phone: document.getElementById('form-phone').value,
    email: document.getElementById('form-email').value
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
