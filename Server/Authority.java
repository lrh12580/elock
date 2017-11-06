/**
 * Created by admin on 2017/2/25.
 */
public class Authority {
    private String guid;
    private String file_path;
    private String authority_number;

    public Authority(String file_path, String authority_number) {
        this.file_path = file_path;
        this.authority_number = authority_number;
    }

    public Authority(String guid, String file_path, String authority_number) {
        this.guid = guid;
        this.file_path = file_path;
        this.authority_number = authority_number;
    }

    public String getGuid() {
        return guid;
    }

    public void setGuid(String guid) {
        this.guid = guid;
    }

    public String getFile_path() {
        return file_path;
    }

    public void setFile_path(String file_path) {
        this.file_path = file_path;
    }

    public String getAuthority_number() {
        return authority_number;
    }

    public void setAuthority_number(String authority_number) {
        this.authority_number = authority_number;
    }

}
