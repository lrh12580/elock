import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;
import java.io.PrintWriter;
import java.net.URLEncoder;
import java.sql.*;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.*;
import java.util.Date;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

public class LoginServlet extends HttpServlet {

    private Connection dbConn;
    private MyEncript myEncript;
    private boolean permission = false;
    private boolean isOver = false;

    /**
     * Constructor of the object.
     */
    public LoginServlet() {
        super();
    }

    /**
     * Destruction of the servlet. <br>
     */
    public void destroy() {
        try {
            dbConn.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
        super.destroy(); // Just puts "destroy" string in log
        // Put your code here
    }

    /**
     * The doGet method of the servlet. <br>
     * <p>
     * This method is called when a form has its tag value method equals to get.
     *
     * @param request  the request send by the client to the server
     * @param response the response send by the server to the client
     * @throws ServletException if an error occurred
     * @throws IOException      if an error occurred
     */
    public void doGet(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        doPost(request, response);
    }

    /**
     * The doPost method of the servlet. <br>
     * <p>
     * This method is called when a form has its tag value method equals to
     * post.
     *
     * @param request  the request send by the client to the server
     * @param response the response send by the server to the client
     * @throws ServletException if an error occurred
     * @throws IOException      if an error occurred
     */

    public void doPost(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        System.out.println("doPost");
        response.setContentType("text/html;charset=utf-8");
        request.setCharacterEncoding("utf-8");
        // 获取请求头
        String header = request.getHeader("test-header");
        if (header != null && !header.equals(""))
            System.out.println("test-header=" + header);
        // 获取请求参数
        PrintWriter out = response.getWriter();
        String model = request.getParameter("model");
        System.out.println(model);
        switch (model) {
            case PostOptions.SEND_PHONE_ID:
                String IMEI1 = request.getParameter("IMEI");
                String user_id1 = calculateId();
                String server_publicKey = myEncript.getPublicKey();
                String client_publicKey = "";
                String client_privateKey = "";
                try {
                    MyEncript client = new MyEncript();
                    client.generateKeyPair();
                    client_publicKey = client.getPublicKey();
                    client_privateKey = client.getPrivateKey();
                    insertUserPhone(user_id1, IMEI1, client.getPublicKey());
                } catch (Exception e) {
                    e.printStackTrace();
                }
                String output1 = "{\"user_id\":\"" + user_id1
                        + "\",\"server_publicKey\":\"" + server_publicKey
                        + "\",\"client_publicKey\":\"" + client_publicKey
                        + "\",\"client_privateKey\":\"" + client_privateKey
                        + "\"}";
                out.println(output1);
                System.out.println("user_id="
                        + new String(user_id1.getBytes("iso-8859-1"), "utf-8"));
                System.out.println("server_publicKey="
                        + new String(server_publicKey.getBytes("iso-8859-1"), "utf-8"));
                System.out.println("client_publicKey="
                        + new String(client_publicKey.getBytes("iso-8859-1"), "utf-8"));
                System.out.println("client_privateKey="
                        + new String(client_privateKey.getBytes("iso-8859-1"), "utf-8"));
                break;
            case PostOptions.REGISTER:
                String user_id2 = request.getParameter("user_id");
                String IMEI2 = request.getParameter("IMEI");
                String clientPublicKey2 = getPhonePublicKey(user_id2, IMEI2);
                String source2 = decrypt(request, clientPublicKey2);

                System.out.println(source2);
                String[] sourceStrArray2 = source2.split("&");
                String name = sourceStrArray2[0].split("=")[1];
                String password = sourceStrArray2[1].split("=")[1];
                String output2 = "";
                if (getUserPassword(name) != "") {
                    output2 = encrypt("status=NO&content=已经注册过了", clientPublicKey2);
                    out.println(output2);
                } else {
                    insertAllusers(user_id2, name, password);
                    createTableHistory(user_id2);
                    createTableAuthority(user_id2);
                    output2 = encrypt("status=OK&content=0", clientPublicKey2);
                    out.println(output2);
                }
                System.out.println("user_id="
                        + new String(user_id2.getBytes("iso-8859-1"), "utf-8"));
                System.out.println("user_name="
                        + new String(name.getBytes("iso-8859-1"), "utf-8"));
                System.out.println("password="
                        + new String(password.getBytes("iso-8859-1"), "utf-8"));

                break;
            case PostOptions.PHONE_LOGIN:
                String user_id3 = request.getParameter("user_id");
                String IMEI3 = request.getParameter("IMEI");
                System.out.println("user_id:" + user_id3);
                System.out.println("IMEI:" + IMEI3);
                String clientPublicKey3 = getPhonePublicKey(user_id3, IMEI3);
                String source3 = decrypt(request, clientPublicKey3);
                String[] sourceStrArray3 = source3.split("&");
                name = sourceStrArray3[0].split("=")[1];
                password = sourceStrArray3[1].split("=")[1];
                String output3 = "";
                if (getUserPassword(name) == "") {
                    output3 = encrypt("status=NO&content=1", clientPublicKey3);
                    out.println(output3);
                    System.out.println("NO");
                } else if (!getUserPassword(name).equals(password)) {
                    output3 = encrypt("status=NO&content=2", clientPublicKey3);
                    out.println(output3);
                    System.out.println("NO");
                } else {
                    output3 = encrypt("status=OK&content=0", clientPublicKey3);
                    out.println(output3);
                    System.out.println("OK");
                }
                System.out.println("user_id="
                        + new String(user_id3.getBytes("iso-8859-1"), "utf-8"));
                System.out.println("user_name="
                        + new String(name.getBytes("iso-8859-1"), "utf-8"));
                System.out.println("password="
                        + new String(password.getBytes("iso-8859-1"), "utf-8"));
                break;
            case PostOptions.SEND_AUTHORITY:
                String user_id4 = request.getParameter("user_id");
                String IMEI4 = request.getParameter("IMEI");
                System.out.println("user_id4:" + user_id4);
                System.out.println("IMEI4:" + IMEI4);
                String clientPublicKey4 = getPhonePublicKey(user_id4, IMEI4);
                List<Authority> authorities = getAuthority(user_id4);
                String output4 = "";
                for (int i = 0; i < authorities.size(); i++) {
                    String guid = authorities.get(i).getGuid();
                    String nickname = getNickname(guid);
                    String file_path = authorities.get(i).getFile_path();
                    String authority_number = authorities.get(i).getAuthority_number();
                    if (i == 0) {
                        output4 = "guid=" + guid + "&file_path=" + file_path
                                + "&nickname=" + nickname
                                + "&authority_number=" + authority_number;
                    } else {
                        output4 += "&guid=" + guid + "&file_path=" + file_path
                                + "&nickname=" + nickname
                                + "&authority_number=" + authority_number;
                    }
                }
                System.out.println(output4);
                output4 = encrypt(output4, clientPublicKey4);
                out.println(output4);
                break;
            case PostOptions.SEND_HISTORY:
                String user_id5 = request.getParameter("user_id");
                String IMEI5 = request.getParameter("IMEI");
                System.out.println("user_id:" + user_id5);
                System.out.println("IMEI:" + IMEI5);
                String clientPublicKey5 = getPhonePublicKey(user_id5, IMEI5);
                String source5 = decrypt(request, clientPublicKey5);

                String[] sourceStrArray5 = source5.split("=");
                String date = sourceStrArray5[1];

                List<History> histories = getHistory(user_id5, date);
                String output5 = "";
                for (int i = 0; i < histories.size(); i++) {
                    String guid = histories.get(i).getGuid();
                    String nickname = getNickname(guid);
                    String file_path = histories.get(i).getFile_path();
                    String authority_number = histories.get(i).getAuthority_number();
                    String operate_time = histories.get(i).getOperate_time();
                    String isPermit = histories.get(i).getIsPermit();
                    String isCheck = histories.get(i).getIsCheck();
                    if (i == 0) {
                        output5 = "guid=" + guid + "&file_path=" + file_path
                                + "&nickname=" + nickname
                                + "&authority_number=" + authority_number
                                + "&operate_time=" + operate_time
                                + "&isPermit=" + isPermit
                                + "&isCheck=" + isCheck;
                    } else {
                        output5 += "&guid=" + guid + "&file_path=" + file_path
                                + "&nickname=" + nickname
                                + "&authority_number=" + authority_number
                                + "&operate_time=" + operate_time
                                + "&isPermit=" + isPermit
                                + "&isCheck=" + isCheck;
                    }
                }
                System.out.println(output5);
                output5 = encrypt(output5, clientPublicKey5);
                out.println(output5);
                break;
            case PostOptions.SYNCHRO:
                String user_id6 = request.getParameter("user_id");
                String IMEI6 = request.getParameter("IMEI");
                System.out.println("user_id:" + user_id6);
                System.out.println("IMEI:" + IMEI6);
                String clientPublicKey6 = getPhonePublicKey(user_id6, IMEI6);
                List<Synchro> synchros = getSynchro(user_id6);
                List<Synchro> updateSynchros = new ArrayList<>();
                String output6 = "";
                for (int i = 0; i < synchros.size(); i++) {
                    String guid = synchros.get(i).getGuid();
                    String nickname = getNickname(guid);
                    String file_path = synchros.get(i).getFile_path();
                    String authority_number = synchros.get(i).getAuthority_number();
                    String operate_date = synchros.get(i).getOperate_date();
                    String operate_time = synchros.get(i).getOperate_time();
                    String isPermit = synchros.get(i).getIsPermit();
                    String isSend = synchros.get(i).getIsSend();
                    if (i == 0) {
                        output6 = "guid=" + guid + "&file_path=" + file_path
                                + "&nickname=" + nickname
                                + "&authority_number=" + authority_number
                                + "&operate_date=" + operate_date
                                + "&operate_time=" + operate_time
                                + "&isPermit=" + isPermit
                                + "&isSend=" + isSend;
                    } else {
                        output6 += "&guid=" + guid + "&file_path=" + file_path
                                + "&nickname=" + nickname
                                + "&authority_number=" + authority_number
                                + "&operate_date=" + operate_date
                                + "&operate_time=" + operate_time
                                + "&isPermit=" + isPermit
                                + "&isSend=" + isSend;
                    }
                    if (isSend.equals("NO")) {
                        updateSynchros.add(synchros.get(i));
                    }
                }
                //System.out.println(output);
                output6 = encrypt(output6, clientPublicKey6);
                out.println(output6);
                updateSend(user_id6, updateSynchros);
                break;
            case PostOptions.VERIFY:
                System.out.println("VERIFYVERIFY");
                String user_id7 = request.getParameter("user_id");
                String IMEI7 = request.getParameter("IMEI");
                System.out.println("user_id:" + user_id7);
                System.out.println("IMEI:" + IMEI7);
                String clientPublicKey7 = getPhonePublicKey(user_id7, IMEI7);
                String source7 = decrypt(request, clientPublicKey7);
                String[] sourceStrArray7 = source7.split("&");
                Synchro synchro = new Synchro(sourceStrArray7[0].split("=")[1], sourceStrArray7[1].split("=")[1],
                        sourceStrArray7[2].split("=")[1], sourceStrArray7[3].split("=")[1],
                        sourceStrArray7[4].split("=")[1], sourceStrArray7[5].split("=")[1],
                        sourceStrArray7[6].split("=")[1]);

                updateHistory(user_id7, synchro);

                String output7 = "";
                output7 = "status=OK&content=0";
                System.out.println(output7);
                output7 = encrypt(output7, clientPublicKey7);
                out.println(output7);
                break;
            case PostOptions.SETNICKNAME:
                String user_id8 = request.getParameter("user_id");
                String IMEI8 = request.getParameter("IMEI");
                System.out.println("user_id:" + user_id8);
                System.out.println("IMEI:" + IMEI8);
                String clientPublicKey8 = getPhonePublicKey(user_id8, IMEI8);
                String source8 = decrypt(request, clientPublicKey8);
                String[] sourceStrArray8 = source8.split("&");
                String guid = sourceStrArray8[0].split("=")[1];
                String nickname = sourceStrArray8[1].split("=")[1];

                updateGuid(guid, nickname);

                String output8 = "";
                output8 = encrypt("status=OK&content=0", clientPublicKey8);
                out.println(output8);
                System.out.println("nicknameOK");

                break;
            case PostOptions.SEND_PC_KEY:
                String clientPublicKey11 = request.getParameter("publicKey");
                String guid11 = request.getParameter("guid");
                String serverPublicKey = myEncript.getPublicKey();
                insertUserPc(guid11, clientPublicKey11);
                String output11 = "";
                output11 = "{\"status\":\"OK\",\"publicKey\":\"" + serverPublicKey + "\"}";
                out.println(output11);
                System.out.println(output11);
                break;
            case PostOptions.PC_LOGIN:
                String guid12 = request.getParameter("guid");
                System.out.println("guid:" + guid12);
                String clientPublicKey12 = getPcPublicKey(guid12);

                String source12 = decrypt(request, clientPublicKey12);
                String[] sourceStrArray12 = source12.split("&");
                name = sourceStrArray12[0].split("=")[1];
                password = sourceStrArray12[1].split("=")[1];
                String output12 = "";
                if (getUserPassword(name) == "") {
                    output12 = encrypt("status=NO&content=1", clientPublicKey12);
                    out.println(output12);
                } else if (!getUserPassword(name).equals(password)) {
                    output12 = encrypt("status=NO&content=2", clientPublicKey12);
                    out.println(output12);
                } else {
                    output12 = encrypt("status=OK&userId=" + getUserId(name), clientPublicKey12);
                    out.println(output12);
                }
                System.out.println("user_name="
                        + new String(name.getBytes("iso-8859-1"), "utf-8"));
                System.out.println("password="
                        + new String(password.getBytes("iso-8859-1"), "utf-8"));
                break;
            case PostOptions.SEND_PC_AUTHORITY:
                String user_id13 = request.getParameter("user_id");
                String guid13 = request.getParameter("guid");
                System.out.println("guid:" + guid13);
                String clientPublicKey13 = getPcPublicKey(guid13);

                authorities = new ArrayList<>();
                authorities = getPcAuthority(user_id13, guid13);
                String output13 = "";
                for (int i = 0; i < authorities.size(); i++) {
                    String file_path = authorities.get(i).getFile_path();
                    String authority_number = authorities.get(i).getAuthority_number();
                    if (i == 0) {
                        output13 = "file_path=" + file_path
                                + "&authority_number=" + authority_number;
                    } else {
                        output13 += "&file_path=" + file_path
                                + "&authority_number=" + authority_number;
                    }
                }
                System.out.println(output13);
                output13 = encrypt(output13, clientPublicKey13);
                out.println(output13);
                break;
            case PostOptions.SEND_PC_HISTORY:
                String user_id14 = request.getParameter("user_id");
                String guid14 = request.getParameter("guid");
                System.out.println("guid:" + guid14);
                String clientPublicKey14 = getPcPublicKey(guid14);

                String source14 = decrypt(request, clientPublicKey14);

                String[] sourceStrArray14 = source14.split("=");
                String date14 = sourceStrArray14[1];

                histories = new ArrayList<>();
                histories = getHistory(user_id14, date14);
                String output14 = "";
                for (int i = 0; i < histories.size(); i++) {
                    String file_path = histories.get(i).getFile_path();
                    String authority_number = histories.get(i).getAuthority_number();
                    String operate_time = histories.get(i).getOperate_time();
                    String isPermit = histories.get(i).getIsPermit();
                    if (i == 0) {
                        output14 = "file_path=" + file_path
                                + "&authority_number=" + authority_number
                                + "&operate_time=" + operate_time
                                + "&isPermit=" + isPermit;
                    } else {
                        output14 += "&file_path=" + file_path
                                + "&authority_number=" + authority_number
                                + "&operate_time=" + operate_time
                                + "&isPermit=" + isPermit;
                    }
                }
                System.out.println(output14);
                output14 = encrypt(output14, clientPublicKey14);
                out.println(output14);
                break;
            case PostOptions.GET_PC_AUTHORITY:
                String user_id15 = request.getParameter("user_id");
                String guid15 = request.getParameter("guid");
                System.out.println("guid:" + guid15);
                String clientPublicKey15 = getPcPublicKey(guid15);

                String source15 = decrypt(request, clientPublicKey15);
                String []sourceStrArray15 = source15.split("&");
                String file_path = sourceStrArray15[0].split("=")[1];
                String authority_number = sourceStrArray15[1].split("=")[1];

                insertAuthority(user_id15, guid15, file_path, authority_number);

                String output15 = "";
                output15 = encrypt("status=OK&content=0", clientPublicKey15);
                out.println(output15);
                break;
            case PostOptions.OPERATE:
                String user_id16 = request.getParameter("user_id");
                String guid16 = request.getParameter("guid");
                System.out.println("guid:" + guid16);
                String clientPublicKey16 = getPcPublicKey(guid16);

                String source16 = decrypt(request, clientPublicKey16);
                String []sourceStrArray16 = source16.split("&");
                file_path = sourceStrArray16[0].split("=")[1];
                authority_number = sourceStrArray16[1].split("=")[1];
                String operate_date = sourceStrArray16[2].split("=")[1];
                String operate_time = sourceStrArray16[3].split("=")[1];

                insertHistory(user_id16, guid16, file_path, authority_number, operate_date, operate_time);

                SimpleDateFormat format = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
                format.setLenient(false);
                try {
                    Date date1 = format.parse(operate_date + " " + operate_time);
                    while (true) {
                        Results result = getHistory(user_id16, guid16, file_path, authority_number, operate_date, operate_time);
                        Date now = new Date();
                        long seconds = (now.getTime() - date1.getTime()) / (1000);
                        System.out.println("seconds:" + seconds);
                        if (result.isCheck() == true || seconds > 60) {
                            permission = result.isPermit();
                            isOver = true;
                            break;
                        }
                        Thread.sleep(2000);
                    }
                } catch (Exception e) {
                    e.printStackTrace();
                }

                String output16 = "";
                System.out.println("over");
                if (permission == true) {
                    if (authority_number.equals("0")) {
                        deleteAuthority(user_id16, guid16, file_path);
                    }
                    output16 = encrypt("status=OK&content=0", clientPublicKey16);
                    out.println(output16);
                } else {
                    output16 = encrypt("status=NO&content=0", clientPublicKey16);
                    out.println(output16);
                }
                if(isOver == true)
                    updateHistory(user_id16, guid16, file_path, authority_number, operate_date, operate_time, permission);
                break;
            case PostOptions.SEND_PC_ALLHISTORY:
                String user_id17 = request.getParameter("user_id");
                String guid17 = request.getParameter("guid");
                System.out.println("guid:" + guid17);
                String clientPublicKey17 = getPcPublicKey(guid17);

                histories = new ArrayList<>();
                histories = getHistory(user_id17);
                String output17 = "";
                for (int i = 0; i < histories.size(); i++) {
                    file_path = histories.get(i).getFile_path();
                    authority_number = histories.get(i).getAuthority_number();
                    operate_date = histories.get(i).getOperate_date();
                    operate_time = histories.get(i).getOperate_time();
                    String isPermit = histories.get(i).getIsPermit();
                    if (i == 0) {
                        output17 = "file_path=" + file_path
                                + "&authority_number=" + authority_number
                                + "&operate_date=" + operate_date
                                + "&operate_time=" + operate_time
                                + "&isPermit=" + isPermit;
                    } else {
                        output17 += "&file_path=" + file_path
                                + "&authority_number=" + authority_number
                                + "&operate_date=" + operate_date
                                + "&operate_time=" + operate_time
                                + "&isPermit=" + isPermit;
                    }
                }
                System.out.println(output17);
                output17 = encrypt(output17, clientPublicKey17);
                out.println(output17);
                break;
            default:
                break;
        }

        out.flush();
        out.close();
    }

    /**
     * Initialization of the servlet. <br>
     *
     * @throws ServletException if an error occurs
     */
    public void init() throws ServletException {
        connection();
        initKey();
    }

    public void initKey() {
        String selSQL = "select * from server_key";
        try {
            Statement statement = dbConn.createStatement();
            ResultSet resultSet = statement.executeQuery(selSQL);
            myEncript = new MyEncript();
            String publicKey = "";
            String privateKey = "";
            if (resultSet.next()) {
                System.out.println("Has publicKey");
                publicKey = resultSet.getString("publicKey");
                privateKey = resultSet.getString("privateKey");
                myEncript.setPublicKey(publicKey);
                myEncript.setPrivateKey(privateKey);
            } else {
                myEncript.generateKeyPair();
                postServerKey(myEncript.getPublicKey(), myEncript.getPrivateKey());
            }
            statement.close();
        } catch (SQLException e) {
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public String decrypt(HttpServletRequest request, String clientPublicKey) {
        String text = request.getParameter("text");
        String password = request.getParameter("password");
        try {
            password = myEncript.decryptRSA(password, myEncript.getPrivateKey(), 2);
            text = myEncript.decryptDES(text, password);
            text = text.replaceAll("\r|\n", "").replaceAll(" ", "").trim();
            JSONObject jsonObject = new JSONObject(text);
            String character = jsonObject.getString("character");
            String source = jsonObject.getString("source");
            character = character.replaceAll("\r|\n", "").replaceAll(" ", "").trim();
            character = myEncript.decryptRSA(character, clientPublicKey, 1);
            character = character.replaceAll("\r|\n", "").replaceAll(" ", "").trim();
            String X = myEncript.SHA1(URLEncoder.encode(source, "utf-8"));
            if (character.equalsIgnoreCase(X)) return source;
            else return "";
        } catch (Exception e) {
            e.printStackTrace();
        }
        return "";
    }

    public String encrypt(String source, String clientPublicKey) {
        String result = "";
        try {
            source = source.replaceAll("\r", "").replaceAll("\n", "").replaceAll(" ", "").trim();
            String X = myEncript.SHA1(URLEncoder.encode(source, "utf-8"));
            String character = myEncript.encryptRSA(X, myEncript.getPrivateKey(), 2);
            String C = "{\"source\":\"" + source
                    + "\",\"character\":\"" + character + "\"}";
            String Q =  generateString(8);
            String D = myEncript.encryptDES(C, Q);
            String P = myEncript.encryptRSA(Q, clientPublicKey, 1);
            result = "{\"text\":\"" + D + "\",\"password\":\"" + P + "\"}";
        } catch (Exception e) {
            e.printStackTrace();
        }
        return result;
    }

    public static String generateString(int length) {
        final String allChar = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        StringBuffer sb = new StringBuffer();
        Random random = new Random();
        for (int i = 0; i < length; i++) {
            sb.append(allChar.charAt(random.nextInt(allChar.length())));
        }
        return sb.toString();
    }

    public void connection() {
        String driverName = "com.microsoft.sqlserver.jdbc.SQLServerDriver";
        String dbURL = "jdbc:sqlserver://localhost:1433;DatabaseName=lyz";
        String userName = "sa";
        String userPwd = "Lrh4189256!@#";

        try {
            Class.forName(driverName);
            dbConn = DriverManager.getConnection(dbURL, userName, userPwd);
            System.out.println("连接数据库成功");
        } catch (Exception e) {
            e.printStackTrace();
            System.out.print("连接失败");
        }
    }

    public String calculateId() {
        String result = "";
        String selSQL = "select count(*) as number from allusers";
        try {
            Statement statement = dbConn.createStatement();
            ResultSet resultSet = statement.executeQuery(selSQL);
            if (resultSet.next()) {
                result = Integer.parseInt(resultSet.getString("number")) + 1 + "";
                statement.close();
                resultSet.close();
                return result;
            }
            resultSet.close();
            statement.close();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return "1";
    }

    public String getNickname(String guid) {
        System.out.println("getNickName");
        String selSQL = "select nickname from user_pc where guid='" + guid +"'" ;
        String result = "";
        try {
            Statement statement = dbConn.createStatement();
            ResultSet resultSet = statement.executeQuery(selSQL);
            if (resultSet.next()) {
                result = resultSet.getString("nickname");
                System.out.println(result);
                resultSet.close();
                statement.close();
                return result;
            }
            resultSet.close();
            statement.close();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return "";
    }

    public void updateGuid(String guid, String nickname) {
        String updateSQL = "update user_pc set nickname='" + nickname +"' where guid='" + guid +"'";
        try {
            Statement statement = dbConn.createStatement();
            statement.executeUpdate(updateSQL);
            statement.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public void insertAllusers(String user_id, String user_name, String password) {
        String insSQL = "insert into allusers(user_id,user_name,password) values(?,?,?)";
        PreparedStatement pstmt = null;
        try {
            pstmt = dbConn.prepareStatement(insSQL);
            pstmt.setString(1, user_id);
            pstmt.setString(2, user_name);
            pstmt.setString(3, password);
            pstmt.executeUpdate();
            pstmt.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public void insertAuthority(String user_id, String guid, String file_path, String authority_number) {
        String insSQL = "insert into user_authority_" + user_id + "(guid,file_path,authority_number) values(?,?,?)";
        PreparedStatement pstmt = null;
        try {
            pstmt = dbConn.prepareStatement(insSQL);
            pstmt.setString(1, guid);
            pstmt.setString(2, file_path);
            pstmt.setString(3, authority_number);
            pstmt.executeUpdate();
            pstmt.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public void insertHistory(String user_id, String guid, String file_path, String authority_number, String operate_date, String operate_time) {
        String insSQL = "insert into user_history_" + user_id
                + "(guid,file_path,authority_number,operate_date,operate_time,isPermit,isCheck,isSend) " +
                "values(?,?,?,?,?,?,?,?)";
        PreparedStatement pstmt = null;
        try {
            pstmt = dbConn.prepareStatement(insSQL);
            pstmt.setString(1, guid);
            pstmt.setString(2, file_path);
            pstmt.setString(3, authority_number);
            pstmt.setString(4, operate_date);
            pstmt.setString(5, operate_time);
            pstmt.setString(6, "NO");
            pstmt.setString(7, "NO");
            pstmt.setString(8, "NO");
            pstmt.executeUpdate();
            pstmt.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public void insertUserPhone(String user_id, String IMEI, String publicKey) {
        String insSQL = "insert into user_phone(user_id,IMEI,publicKey) values(?,?,?)";
        PreparedStatement pstmt = null;
        try {
            pstmt = dbConn.prepareStatement(insSQL);
            pstmt.setString(1, user_id);
            pstmt.setString(2, IMEI);
            pstmt.setString(3, publicKey);
            pstmt.executeUpdate();
            pstmt.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public void insertUserPc(String guid, String publicKey) {
        String insSQL = "insert into user_pc(guid,publicKey,nickname) values(?,?,?)";
        PreparedStatement pstmt = null;
        try {
            pstmt = dbConn.prepareStatement(insSQL);
            pstmt.setString(1, guid);
            pstmt.setString(2, publicKey);
            pstmt.setString(3, "undefined");
            pstmt.executeUpdate();
            pstmt.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public void deleteAuthority(String user_id, String guid, String file_path) {
        String deleteSQL = "delete from user_authority_" + user_id +
                            " where guid='" +  guid + "'" +
                            " and file_path='" + file_path + "'";
        try {
            Statement statement = dbConn.createStatement();
            statement.executeUpdate(deleteSQL);
            statement.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public void updateAllusers(String user_id, String user_name, String password) {
        String updateSQL = "update allusers set password='" +
                password + "' where user_id='" + user_id + "' and user_name='" + user_name + "'";
        Statement statement = null;
        try {
            statement = dbConn.createStatement();
            statement.executeUpdate(updateSQL);
            statement.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public void updateSend(String user_id, List<Synchro> synchros) {

        for (int i = 0; i < synchros.size(); i++) {
            String updateSQL = "update user_history_" + user_id
                    + " set isSend='YES'"
                    + " where guid='" + synchros.get(i).getGuid() + "' and file_path='" + synchros.get(i).getFile_path()
                    + "' and authority_number='" + synchros.get(i).getAuthority_number() + "' and operate_date='" + synchros.get(i).getOperate_date()
                    + "' and operate_time='" + synchros.get(i).getOperate_time() + "'";
            System.out.println(updateSQL);
            try {
                Statement statement = dbConn.createStatement();
                statement.executeUpdate(updateSQL);
                statement.close();
            } catch (SQLException e) {
                e.printStackTrace();
            }
        }
    }

    public void updateHistory(String user_id, Synchro synchro) {
        String updateSQL = "update user_history_" + user_id
                + " set isPermit='" + synchro.getIsPermit() + "'"
                + " where guid='" + synchro.getGuid() + "' and file_path='" + synchro.getFile_path()
                + "' and authority_number='" + synchro.getAuthority_number() + "' and operate_date='" + synchro.getOperate_date()
                + "' and operate_time='" + synchro.getOperate_time() + "'";
        System.out.println(updateSQL);
        Statement statement = null;
        try {
            statement = dbConn.createStatement();
            statement.executeUpdate(updateSQL);
            statement.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
        updateSQL = "update user_history_" + user_id
                + " set isCheck='YES'"
                + " where guid='" + synchro.getGuid() + "' and file_path='" + synchro.getFile_path()
                + "' and authority_number='" + synchro.getAuthority_number() + "' and operate_date='" + synchro.getOperate_date()
                + "' and operate_time='" + synchro.getOperate_time() + "'";
        System.out.println(updateSQL);
        try {
            statement = dbConn.createStatement();
            statement.executeUpdate(updateSQL);
            statement.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public void updateHistory(String user_id, String guid, String file_path, String authority_number, String operate_date, String operate_time, boolean isPermit) {
        String updatePermit = "";
        if (isPermit == true) updatePermit = "YES";
        else updatePermit = "NO";
        String updateSQL = "update user_history_" + user_id
                + " set isPermit='" + updatePermit + "'"
                + " where guid='" + guid + "' and file_path='" + file_path
                + "' and authority_number='" + authority_number + "' and operate_date='" + operate_date
                + "' and operate_time='" + operate_time + "'";
        System.out.println(updateSQL);
        Statement statement = null;
        try {
            statement = dbConn.createStatement();
            statement.executeUpdate(updateSQL);
            statement.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
        updateSQL = "update user_history_" + user_id
                + " set isCheck='YES'"
                + " where guid='" + guid + "' and file_path='" + file_path
                + "' and authority_number='" + authority_number + "' and operate_date='" + operate_date
                + "' and operate_time='" + operate_time + "'";
        System.out.println(updateSQL);
        try {
            statement = dbConn.createStatement();
            statement.executeUpdate(updateSQL);
            statement.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }


    public String getUserPassword(String user_name) {
        String result = "";
        String selSQL = "select password from allusers where user_name='" + user_name + "'";
        try {
            Statement statement = dbConn.createStatement();
            ResultSet resultSet = statement.executeQuery(selSQL);
            if (resultSet.next()) {
                result = resultSet.getString("password");
                resultSet.close();
                statement.close();
                return result;
            }
            resultSet.close();
            statement.close();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return "";
    }

    public String getUserId(String user_name) {
        String result = "";
        String selSQL = "select user_id from allusers where user_name='" + user_name + "'";
        try {
            Statement statement = dbConn.createStatement();
            ResultSet resultSet = statement.executeQuery(selSQL);
            if (resultSet.next()) {
                result = resultSet.getString("user_id");
                resultSet.close();
                statement.close();
                return result;
            }
            resultSet.close();
            statement.close();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return "";
    }

    public String getPcPublicKey(String guid) {
        String result = "";
        String selSQL = "select publicKey from user_pc where guid='" + guid + "'";
        try {
            Statement statement = dbConn.createStatement();
            ResultSet resultSet = statement.executeQuery(selSQL);
            if (resultSet.next()) {
                result = resultSet.getString("publicKey");
                resultSet.close();
                statement.close();
                return result;
            }
            resultSet.close();
            statement.close();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return "";
    }

    public String getPhonePublicKey(String id, String IMEI) {
        String result = "";
        String selSQL = "select * from user_phone where user_id='" + id
                + "' and IMEI='" + IMEI + "'";
        try {
            Statement statement = dbConn.createStatement();
            ResultSet resultSet = statement.executeQuery(selSQL);
            if (resultSet.next()) {
                result = resultSet.getString("publicKey");
                resultSet.close();
                statement.close();
                return result;
            }
            resultSet.close();
            statement.close();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return "";
    }

    public Results getHistory(String user_id, String guid, String file_path, String authority_number, String operate_date, String operate_time) {
        Results result = new Results();
        String selSQL = "select isPermit,isCheck from user_history_" + user_id
                + " where guid='" + guid + "'"
                + " and file_path='" + file_path + "'"
                + " and authority_number='" + authority_number + "'"
                + " and operate_date='" + operate_date + "'"
                + " and operate_time='" + operate_time + "'";
        try {
            Statement statement = dbConn.createStatement();
            ResultSet resultSet = statement.executeQuery(selSQL);
            while (resultSet.next()) {
                String isPermit = resultSet.getString("isPermit");
                String isCheck = resultSet.getString("isCheck");
                if (isPermit.equals("YES")) result.setPermit(true);
                else result.setPermit(false);
                if (isCheck.equals("YES")) result.setCheck(true);
                else result.setCheck(false);
            }
            resultSet.close();
            statement.close();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return result;
    }

    public List<Authority> getAuthority(String user_id) {
        List<Authority> authorities = new ArrayList<>();
        String selSQL = "select * from user_authority_" + user_id;
        try {
            Statement statement = dbConn.createStatement();
            ResultSet resultSet = statement.executeQuery(selSQL);
            while (resultSet.next()) {
                String guid = resultSet.getString("guid");
                String file_path = resultSet.getString("file_path");
                String authority_number = resultSet.getString("authority_number");
                authorities.add(new Authority(guid, file_path, authority_number));
            }
            resultSet.close();
            statement.close();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return authorities;
    }

    public List<Authority> getPcAuthority(String user_id, String cup_id) {
        List<Authority> authorities = new ArrayList<>();
        String selSQL = "select * from user_authority_" + user_id + " where guid='" + cup_id + "'";
        try {
            Statement statement = dbConn.createStatement();
            ResultSet resultSet = statement.executeQuery(selSQL);
            while (resultSet.next()) {
                String file_path = resultSet.getString("file_path");
                String authority_number = resultSet.getString("authority_number");
                authorities.add(new Authority(file_path, authority_number));
            }
            resultSet.close();
            statement.close();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return authorities;
    }

    public List<History> getHistory(String user_id) {
        List<History> histories = new ArrayList<>();
        String selSQL = "select * from user_history_" + user_id;
        try {
            Statement statement = dbConn.createStatement();
            ResultSet resultSet = statement.executeQuery(selSQL);
            while (resultSet.next()) {
                String guid = resultSet.getString("guid");
                String file_path = resultSet.getString("file_path");
                String authority_number = resultSet.getString("authority_number");
                String operate_date = resultSet.getString("operate_date");
                String operate_time = resultSet.getString("operate_time");
                String isPermit = resultSet.getString("isPermit");
                String isCheck = resultSet.getString("isCheck");
                histories.add(new History(guid, file_path, authority_number, operate_date, operate_time, isPermit, isCheck));
            }
            resultSet.close();
            statement.close();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return histories;
    }

    public List<History> getHistory(String user_id, String date) {
        List<History> histories = new ArrayList<>();
        String selSQL = "select * from user_history_" + user_id + " where operate_date='" + date + "'";
        try {
            Statement statement = dbConn.createStatement();
            ResultSet resultSet = statement.executeQuery(selSQL);
            while (resultSet.next()) {
                String guid = resultSet.getString("guid");
                String file_path = resultSet.getString("file_path");
                String authority_number = resultSet.getString("authority_number");
                String operate_time = resultSet.getString("operate_time");
                String isPermit = resultSet.getString("isPermit");
                String isCheck = resultSet.getString("isCheck");
                histories.add(new History(guid, file_path, authority_number, operate_time, isPermit, isCheck));
            }
            resultSet.close();
            statement.close();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return histories;
    }

    public List<Synchro> getSynchro(String user_id) {
        List<Synchro> synchros = new ArrayList<>();
        String selSQL = "select * from user_history_" + user_id + " where isCheck='NO'";
        try {
            Statement statement = dbConn.createStatement();
            ResultSet resultSet = statement.executeQuery(selSQL);
            while (resultSet.next()) {
                String guid = resultSet.getString("guid");
                String file_path = resultSet.getString("file_path");
                String authority_number = resultSet.getString("authority_number");
                String operate_date = resultSet.getString("operate_date");
                String operate_time = resultSet.getString("operate_time");
                String isPermit = resultSet.getString("isPermit");
                String isSend = resultSet.getString("isSend");
                Synchro synchro = new Synchro(guid, file_path, authority_number, operate_date, operate_time, isPermit, isSend);
                synchros.add(synchro);
            }
            resultSet.close();
            statement.close();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return synchros;
    }

    public void postServerKey(String publicKey, String privateKey) {
        String insSQL = "insert into dbo.server_key(publicKey,privateKey) values(?,?)";
        PreparedStatement pstmt = null;
        try {
            pstmt = dbConn.prepareStatement(insSQL);
            pstmt.setString(1, publicKey);
            pstmt.setString(2, privateKey);
            pstmt.executeUpdate();
            pstmt.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public void createTableAuthority(String id) {
        String createSQL = "create table user_authority_" + id
                + "(guid varchar(50) not null, "
                + "file_path varchar(100) not null, "
                + "authority_number varchar(50) not null, "
                + "primary key(guid, file_path, authority_number))";
        Statement statement = null;
        try {
            statement = dbConn.createStatement();
            statement.executeUpdate(createSQL);
            statement.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public void createTableHistory(String id) {
        String createSQL = "create table user_history_" + id
                + "(guid varchar(50) not null, "
                + "file_path varchar(100) not null, "
                + "authority_number varchar(50) not null, "
                + "operate_date varchar(50) not null, "
                + "operate_time varchar(50) not null, "
                + "isPermit varchar(10), "
                + "isCheck varchar(10) not null, "
                + "isSend varchar(10) not null, "
                + "primary key(guid, file_path, authority_number, operate_date, operate_time))";
        Statement statement = null;
        try {
            statement = dbConn.createStatement();
            statement.executeUpdate(createSQL);
            statement.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

}
