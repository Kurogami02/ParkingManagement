<template>
  <div class="Form1" style="padding: 25px;">
    <div style="margin-bottom: 30px; display: flex;">
      <div style="margin-top: 30px;">
  <!-- Button để hiển thị form thêm mới -->
  <v-btn @click="showForm = !showForm" max-width="600px">Thêm thông tin</v-btn>
  
  <!-- Form thêm mới sinh viên -->
  <v-dialog v-model="showForm" max-width="600px">
    <v-card style="padding: 15px;">
      <h3>Thêm thông tin</h3>
      <v-text-field label="Đại lý ID" v-model="BaiXeID"></v-text-field>
      <v-text-field label="Ngày kiểm kê" v-model="NgayDatISO" type="text"></v-text-field>
      <v-text-field label="Doanh thu" v-model="BienSo"></v-text-field>  
      <v-select
        v-model="TrangThai"
        :items="['Đã đặt', 'Trống']"
        label="Trạng thái"
      ></v-select>
      <v-btn @click="doPost" style="background-color: rgb(15, 238, 164);">Thêm mới</v-btn>
      <span>{{ message }}</span>
    </v-card>
  </v-dialog>
    </div>
    <div style="margin-left: 1000px; width: 700px; margin-top: 20px;">
      <v-text-field
      v-model="searchTrangThai"
      append-icon="mdi-magnify"
      label="Tìm kiếm"
      single-line
      hide-details
      @click:append="searchByTrangThai"
    ></v-text-field>
   </div>
    </div>
    <table>
      <thead>
        <tr>
          <th>ID Đại lý</th>
          <th>Ngày kiểm kê</th>
          <th>Doanh thu</th>
          <th>Trạng thái</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="bx in listBaiXe" :key="bx.ID">
          <td>{{ bx.BaiXeID }}</td>
          <td>{{ bx.NgayDat }}</td>
          <td>{{ bx.BienSo }}</td>
          <td>{{ bx.TrangThai }}</td>
          <td style="text-align: center;">
            <v-btn icon @click="editBaiXe(bx)" class="btn btn-outline-primary btn-sm">
              <v-icon>mdi-pencil</v-icon>
            </v-btn>
            <v-btn icon @click="confirmDelete(bx.ID)" class="btn btn-outline-danger btn-sm">
              <v-icon>mdi-delete</v-icon>
            </v-btn>
          </td>
        </tr>
      </tbody>
    </table>

    <!-- Hiển thị lỗi -->
    <ul v-if="errors.length">
      <li v-for="error in errors" :key="error.message">{{ error.message }}</li>
    </ul>

    <!-- Dialog sửa thông tin -->
    <v-dialog v-model="editDialog" max-width="600px">
      <v-card>
        <v-card-title>
          Sửa Thông Tin Bãi Xe
        </v-card-title>
        <v-card-text>
          <v-text-field v-model="editedBaiXe.BaiXeID" label="Đại lý ID"></v-text-field>
          <v-text-field v-model="editedBaiXe.NgayDat" label="Ngày kiểm kê"></v-text-field>
          <v-text-field v-model="editedBaiXe.BienSo" label="Doanh thu"></v-text-field>
          <v-select
            v-model="editedBaiXe.TrangThai"
            :items="['Đã xác nhận', 'Chưa xác nhận']"
            label="Trạng thái"
           ></v-select>
          <!-- Thêm các trường sửa thông tin tại đây -->
        </v-card-text>
        <v-card-actions>
          <v-btn @click="saveEdit" color="primary">Lưu</v-btn>
          <v-btn @click="cancelEdit">Hủy</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      listBaiXe: [],
      errors: [],
      editDialog: false,
      editedBaiXe: {},
      showForm: false,
      BaiXeID:"",
      NgayDat: "",
      BienSo: "",
      TrangThai: "",
      DanhMucXeID: "",
      ID: "",
      message: "",
      NgayDat: new Date(),
      searchTrangThai: ''
    };
  },
  created() {
    this.fetchBaiXes();
  },
  computed: {
    NgayDatISO: {
      get() {
        // Chuyển đổi ngày thành chuỗi ISO 8601
        return this.NgayDat.toISOString().slice(0, 19); // Cắt bớt phần thừa để loại bỏ giờ và phút
      },
      set(value) {
        // Chuyển đổi chuỗi ISO 8601 thành ngày
        this.NgayDat = new Date(value);
      },
    },
  },
  methods: {
    fetchBaiXes() {
      axios.get('https://localhost:7117/api/DatXe/')
        .then(response => {
          this.listBaiXe = response.data;
        })
        .catch(e => {
          this.errors.push(e);
        });
    },
    confirmDelete(ID) {
      if (confirm("Bạn có muốn xóa Bãi Xe có id: " + ID + " không?")) {
        this.deleteBaiXe(ID);
      }
    },
    deleteBaiXe(ID) {
      axios.delete('https://localhost:7117/api/DatXe/' + ID)
        .then(response => {
          // Xóa thành công, cập nhật lại danh sách
          this.fetchBaiXes();
        })
        .catch(e => {
          this.errors.push(e);
        });
    },
    editBaiXe(bx) {
      this.editedBaiXe = { ...bx };
      this.editDialog = true;
    },
    saveEdit() {
      this.updateBaiXe(this.editedBaiXe);
      this.editDialog = false;
    },
    cancelEdit() {
      this.editDialog = false;
    },
    updateBaiXe(baiXe) {
      axios.put('https://localhost:7117/api/DatXe/' + baiXe.ID, baiXe)
        .then(response => {
          // Cập nhật thành công, cập nhật lại danh sách
          this.fetchBaiXes();
        })
        .catch(e => {
          this.errors.push(e);
        });
    },
    doPost() {
          axios.post('https://localhost:7117/api/DatXe/', {
            BaiXeID: this.BaiXeID,
            NgayDat: this.NgayDat,
            BienSo: this.BienSo,
            DanhMucXeID: this.DanhMucXeID,
            TrangThai: this.TrangThai
          })
          .then(response => {
            this.message = "Thêm mới thành công!";
            // Cập nhật danh sách sinh viên sau khi thêm mới
          })
          .catch(e => {
            this.message = "Thêm mới thất bại!";
            console.error(e);
          });
        },
        searchByTrangThai() {
  if (this.searchTrangThai.trim() === '') {
    // Nếu giá trị vị trí trống, hiển thị tất cả danh sách
    this.fetchBaiXes();
  } else {
    // Nếu có giá trị vị trí, thực hiện tìm kiếm theo vị trí
    const TrangThai = this.searchTrangThai;
    axios.get('https://localhost:7117/api/DatXe', { params: { TrangThai } })
      .then(response => {
        // Kiểm tra xem response.data có phải là một mảng không
        if (Array.isArray(response.data)) {
          this.listBaiXe = response.data;
        } else {
          // Nếu không phải mảng, gán response.data xác nhận một mảng để hiển thị
          this.listBaiXe = [response.data];
        }
      })
      .catch(e => {
        this.errors.push(e);
      });
  }
}
  }
};
</script>


<style>
table {
  border-collapse: collapse;
  width: 100%;
}

th, td {
  border: 1px solid #dddddd;
  text-align: left;
  padding: 8px;
}

th {
  background-color: #f2f2f2;
}

tr:nth-child(even) {
  background-color: #f2f2f2;
}

tr:hover {
  background-color: #d9d9d9;
}

ul {
  list-style: none;
  padding: 0;
}

li {
  margin-bottom: 10px;
}
.modal {
display: none;
position: fixed;
z-index: 1;
left: 0;
top: 0;
width: 100%;
height: 100%;
overflow: auto;
background-color: rgba(0, 0, 0, 0.4);
}

.modal-content {
background-color: #fff;
margin: 15% auto;
padding: 20px;
width: 70%;
}

.modal-content h3 {
text-align: center;
}

.modal-content v-text-field {
width: 100%;
}

.modal-content button {
display: block;
margin: 10px auto;
}

/* Hiển thị modal dialog khi biến showEditDialog là true */
.modal.show {
display: block;
}
</style>